using AppLogic.Mapper;
using AppLogic.UseCase.PurchaseUC;
using BusinessLogic.Entities;
using ClosedXML.Excel;
using MonitoreoDeArchivos.ApiCalls;
using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.Purchase;
using SharedUseCase.DTOs.PurchaseProduct;
using SharedUseCase.DTOs.Reports;
using SharedUseCase.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoreoDeArchivos.fromXlsx
{
    internal class xlsxConverterPurchases
    {

        private List<EntryDto> entries = new List<EntryDto>();
        public List<PurchaseDto> ConvertFromXlsx(string filePath)
        {

            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                throw new FileNotFoundException("El archivo especificado no existe.", filePath);
            }

            //Limpiar compras para la versión de pruebas ELIMINAR DESPUÉS
            try
            {
               // purchasesCalls.ClearPurchases();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al limpiar los subproductos: {ex.Message}");
            }

            var purchases = new List<PurchaseDto>();
            using (var workbook = new XLWorkbook(filePath))
            {

                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Salta encabezado

                int totalLines = rows.Count() -1; // Total de líneas procesadas, menos encabezado
                int processedLines = 0; // Inicialmente, no se han procesado líneas

                foreach (var row in rows)
                {

                    try { 
                    if (int.Parse(row.Cell(1).GetString()) == 0)
                    {
                        continue;  // Si el ID es 0, se asume que no hay datos de cliente guardado
                    }
                    }catch(Exception e)
                    {
                        continue;
                    }

                    var line = new string[]
                    {
                        row.Cell(1).GetString(),
                        row.Cell(2).GetString(),
                        row.Cell(3).GetString(),
                        row.Cell(4).GetString(),
                        row.Cell(5).GetString(),
                        row.Cell(6).GetString(),
                        row.Cell(7).GetString(),
                    };
                    
                    // Await the asynchronous method to resolve the Task<SubProductDto> into SubProductDto
                    var purchase = ConvertLine(line);
                    purchases.Add(purchase);
                    processedLines++; // Incrementa el contador de líneas procesadas
                }

                ReportDto report = new ReportDto(
                Id: 0,
                Date: DateTime.Now,
                Entries: entries,
                TotalLines: totalLines,
                ProcessedLines: processedLines,
                type: "Purchases"
                );

                reportsCalls.AddReport(report); // Agrega el reporte a la base de datos

            }
            return purchases;
        }

        private PurchaseDto ConvertLine(string[] line)
        {
            try { 
            if (line.Length < 6) {
                EntryDto entry = new EntryDto(
                    Id: 0,
                    Description: "Línea incompleta",
                    Date: DateTime.Now,
                    Type: "Error",
                    Status: "Fallido",
                    ErrorMessage: "La línea no contiene suficientes datos para convertir a PurchaseDto."
                );
                entries.Add(entry);
                throw new ArgumentException("Line does not contain enough data to convert to PurchaseDto.");
            }

            if (line[2].Length != 8)//Línea en caso de que se quiera limitar a los clientes solo con cédula de identidad, sin rut de empresa
            {
                Console.WriteLine($"La venta no es de un cliente con CI. CIF asociado: {line[2]}");
                EntryDto entry = new EntryDto(
                    Id: 0,
                    Description: "Venta no válida",
                    Date: DateTime.Now,
                    Type: "Excepción",
                    Status: "No añadido",
                    ErrorMessage: $"La venta no es de un cliente con CI. CIF asociado: {line[2]}"
                );
                    entries.Add(entry);
                    return null;
            }

            UserDto client =  userCalls.GetClientByCi(line[2]);
            ProductDto product =  productsCalls.GetProductByCode(line[4]);

            if (client == null)
            {
                Console.WriteLine($"No se encontró el cliente con CI: {line[2]}");
                    EntryDto entry = new EntryDto(
                    Id: 0,
                    Description: "Cliente no encontrado",
                    Date: DateTime.Now,
                    Type: "Excepcion",
                    Status: "No añadido",
                    ErrorMessage: $"No se encontró el cliente con CI: {line[2]}"
                );
                    entries.Add(entry);
                    return null;
            }

            PurchaseDto lastPurchase =  GetLastPurchaseMade();
            
            if (lastPurchase != null && lastPurchase.Client == client)
            {
                PurchaseProductDto purchaseProduct = new PurchaseProductDto(
                productId: product.id,
                quantity: 1,
                purchaseId: lastPurchase.Id
                );

                // Create a new PurchaseDto with updated Amount and Products
                var updatedPurchase = new PurchaseDto(
                    Id: lastPurchase.Id,
                    Client: lastPurchase.Client,
                    Amount: lastPurchase.Amount + double.Parse(line[3]),
                    PointsGenerated: lastPurchase.PointsGenerated,
                    PurchaseProducts: new List<PurchaseProductDto>(lastPurchase.PurchaseProducts) { purchaseProduct },
                    Date: lastPurchase.Date
                );

                    EntryDto entry = new EntryDto(
                        Id: 0,
                        Description: $"Nueva compra actualizada para el cliente {client.Ci} - {client.Name}. Producto: {product.name}. Precio: {lastPurchase.Amount + double.Parse(line[3])}",
                        Date: DateTime.Now,
                        Type: "Compra",
                        Status: "Ok",
                        ErrorMessage: ""
                    );
                    entries.Add(entry);

                    UpdatePurchase(updatedPurchase);
                
                return  GetLastPurchaseMade();
            }
            else
            {

                PurchaseProductDto purchaseProduct = new PurchaseProductDto(
                productId: product.id,
                quantity: 1,
                purchaseId: 0
                );

                var products = new List<PurchaseProductDto> { purchaseProduct };

                PurchaseDto newPurchase  = new PurchaseDto(
                    Id: 0,
                    Client: client,
                    Amount: double.Parse(line[3]),
                    PointsGenerated: 0,
                    PurchaseProducts: products,
                    Date: DateTime.Now
                );

                    EntryDto entry = new EntryDto(
                        Id: 0,
                        Description: $"Nueva compra agregada para el cliente {client.Ci} - {client.Name}. Producto: {product.name}. Precio: {newPurchase.Amount}",
                        Date: DateTime.Now,
                        Type: "Compra",
                        Status: "Ok",
                        ErrorMessage: ""
                    );
                    entries.Add(entry);

                    // Add the purchase to the database
                    return AddPurchase(newPurchase);
            }
            }catch(Exception ex) {
                // Handle the exception and log the error
                Console.WriteLine($"Error al convertir la línea: {ex.Message}");
                EntryDto entry = new EntryDto(
                    Id: 0,
                    Description: "Error al convertir la línea",
                    Date: DateTime.Now,
                    Type: "Error",
                    Status: "Fallido",
                    ErrorMessage: ex.Message
                );
                entries.Add(entry);
                return null; // Return null in case of an error
            }
        }

        private  static PurchaseDto AddPurchase(PurchaseDto pur)
        {
            try
            {

                var id = purchasesCalls.AddSubPurchase(pur);
                if (id != null)
                {
                    PurchaseDto purchase = purchasesCalls.getPurchaseById(int.Parse(id)); 
                    Console.WriteLine($"Venta agregada: Id: {purchase.Id} - Monto: {purchase.Amount} - Cliente: {purchase.Client.Ci} - {purchase.Client.Name}. Puntos generados: {purchase.PointsGenerated}");
                    return purchase; // Return the added purchase
                }
                else
                {
                    Console.WriteLine("No se pudo agregar la venta.");
                    return null; // Return null if the addition failed
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar la venta: {ex.Message}");
                return null; // Return null in case of an error
            }
        }

        private void UpdatePurchase(PurchaseDto updatedPurchase)
        {
            try
            {
                purchasesCalls.updatePurchase(updatedPurchase);
                Console.WriteLine($"Compra actualizada: {updatedPurchase.Id} - {updatedPurchase.Amount} - {updatedPurchase.Client.Ci} - {updatedPurchase.Client.Name}. Puntos generados: {updatedPurchase.PointsGenerated}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la compra: {ex.Message}");
            }
        }

        private  PurchaseDto GetLastPurchaseMade()
        {
            try
            {
                var purchases = purchasesCalls.GetAllPurchases();
                if (purchases != null && purchases.Count > 0)
                {
                    return purchases.Last(); // Devuelve la última compra realizada
                }
                return null; // No hay compras
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la última compra: {ex.Message}");
                return null; // Manejo de error, puedes lanzar una excepción o devolver null
            }
        }
    }
}
