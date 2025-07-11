using AppLogic.Mapper;
using ClosedXML.Excel;
using MonitoreoDeArchivos.ApiCalls;
using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.Purchase;
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
        public List<PurchaseDto> ConvertFromXlsx(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                throw new FileNotFoundException("El archivo especificado no existe.", filePath);
            }

            //Limpiar compras para la versión de pruebas ELIMINAR DESPUÉS
            try
            {
                purchasesCalls.ClearPurchases().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al limpiar los subproductos: {ex.Message}");
            }

            var purchases = new List<PurchaseDto>();
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed().Skip(2); // Salta encabezado
                foreach (var row in rows)
                {
                    if (int.Parse(row.Cell(1).GetString()) == 0)
                    {
                        continue;  // Si el ID es 0, se asume que no hay datos de cliente guardado
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
                    var purchase = ConvertLine(line).Result;
                    purchases.Add(purchase);

                }
            }
            return purchases;
        }

        private async Task<PurchaseDto> ConvertLine(string[] line)
        {
            if (line.Length < 6)
                throw new ArgumentException("Line does not contain enough data to convert to PurchaseDto.");

            if (line[2].Length != 8)//Línea en caso de que se quiera limitar a los clientes solo con cédula de identidad, sin rut de empresa
            {
                Console.WriteLine($"La venta no es de un cliente con CI. CIF asociado: {line[2]}");
                return null;
            }

            UserDto client = await userCalls.GetClientByCi(line[2]);
            ProductDto product = await productsCalls.GetProductByCode(line[4]);

            if (client == null)
            {
                Console.WriteLine($"No se encontró el cliente con CI: {line[2]}");
                return null;
            }

            PurchaseDto lastPurchase = await GetLastPurchaseMade();
            if (lastPurchase != null && lastPurchase.Client == client)
            {
                // Create a new PurchaseDto with updated Amount and Products
                var updatedPurchase = new PurchaseDto(
                    Id: lastPurchase.Id,
                    Client: lastPurchase.Client,
                    Amount: lastPurchase.Amount + double.Parse(line[3]),
                    PointsGenerated: lastPurchase.PointsGenerated,
                    Products: new List<ProductDto>(lastPurchase.Products) { product }
                );

                purchasesCalls.updatePurchase(updatedPurchase);
                return await GetLastPurchaseMade();
            }
            else
            {
                var products = new List<ProductDto> { product };

                PurchaseDto newPurchase  = new PurchaseDto(
                    Id: 0,
                    Client: client,
                    Amount: double.Parse(line[3]),
                    PointsGenerated: 0,
                    Products: products
                );

                // Add the purchase to the database
                return await AddPurchase(newPurchase);
            }
        }

        private async static Task<PurchaseDto> AddPurchase(PurchaseDto pur)
        {
            try
            {
                int id = int.Parse(await purchasesCalls.AddSubPurchaseAsync(pur));
                if (id != null)
                {
                    
                    PurchaseDto purActualizada = await purchasesCalls.getPurchaseById(id); 
                    Console.WriteLine($"Venta agregada: {purActualizada}");

                    return purActualizada; // Return the subproduct if added successfully

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

        private async Task<PurchaseDto> GetLastPurchaseMade()
        {
            try
            {
                var purchases = await purchasesCalls.GetAllPurchases();
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
