using SharedUseCase.DTOs.Product;
using OfficeOpenXml;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using MonitoreoDeArchivos.ApiCalls;
using System.Text.Json;
using System.Threading.Tasks;
using AppLogic.Mapper;
using BusinessLogic.Entities;
using SharedUseCase.DTOs.Reports;




namespace MonitoreoDeArchivos.fromXlsx
{
    internal class xlsxConverterSubProducts
    {

        private List<EntryDto> entries = new List<EntryDto>();
        public List<SubProductDto> ConvertFromXlsx(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                throw new FileNotFoundException("El archivo especificado no existe.", filePath);
            }

            var subProducts = new List<SubProductDto>();
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                int filas = worksheet.RowCount();
                var rows = worksheet.RangeUsed().RowsUsed().Skip(2); // Salta encabezado

                int totalLines = rows.Count() - 1; // Total de líneas procesadas, menos encabezado
                int processedLines = 0; // Inicialmente, no se han procesado líneas

                foreach (var row in rows)
                {
                    var line = new string[]
                    {
                        row.Cell(1).GetString(),
                        row.Cell(2).GetString(),
                        row.Cell(3).GetString(),
                        row.Cell(4).GetString(),
                        row.Cell(5).GetString(),
                        row.Cell(6).GetString(),
                        row.Cell(7).GetString(),
                        row.Cell(8).GetString(),
                        row.Cell(9).GetString(),
                        row.Cell(10).GetString(),
                        row.Cell(11).GetString(),
                        row.Cell(12).GetString(),
                        row.Cell(13).GetString(),
                        row.Cell(14).GetString(),
                        row.Cell(15).GetString(),
                        row.Cell(16).GetString(),
                        row.Cell(17).GetString(),
                    };

                    // Await the asynchronous method to resolve the Task<SubProductDto> into SubProductDto
                    var subProduct = ConvertLine(line).Result;
                    subProducts.Add(subProduct);
                }

                ReportDto report = new ReportDto(
                Id: 0,
                Date: DateTime.Now,
                Entries: entries,
                TotalLines: totalLines,
                ProcessedLines: processedLines,
                type: "Products"
                );

                reportsCalls.AddReport(report); // Agrega el reporte a la base de datos
            }
            return subProducts;
        }

        public async Task<SubProductDto> ConvertLine(string[] line)
        {
            try { 
            // Assuming the line contains data in a specific format, e.g.:
            // [0] = ProductId, [1] = Name, [2] = Description, [3] = Price, [4] = Stock
            if (line.Length < 10) {

                EntryDto entry = new EntryDto(
                    Id: 0,
                    Description: "Línea incompleta",
                    Date: DateTime.Now,
                    Type: "Error",
                    Status: "Fallido",
                    ErrorMessage: "La línea no contiene suficientes datos para convertir a SubProductDto."
                );

                entries.Add(entry);
                throw new ArgumentException("Line does not contain enough data to convert to ProductDto.");
            }
            int stockPdelE = int.Parse(line[11]);
            int stockCol = int.Parse(line[12]);
            int stockPay = int.Parse(line[13]);
            int stockPeat = int.Parse(line[14]);
            int stockSal = int.Parse(line[15]);
            int stockTotal = stockPdelE + stockPay + stockCol + stockPeat + stockSal;
            if (stockTotal > 0)
            {
                SubProductDto sub = new SubProductDto(
                Id: 0, // Assuming ID is auto-generated or not provided in the line
                ProductId: 0, // Assuming ProductId is at index 0
                productCode: line[7], // Assuming productCode is at index 
                Name: line[3],
                Price: double.TryParse(line[16], out double price) ? double.Parse(line[16]) : 0.0,
                Color: line[2],
                Size: line[1],
                Year: line[9],
                Images: null,
                Season: line[10],
                genre: line[8],
                brand: line[6],
                type: line[5],
                stockPdelE: stockPdelE > 0 ? stockPdelE : 0, // Set to null if stock is 0
                stockCol: stockCol > 0 ? stockCol : 0, // Set to null if stock is 0
                stockPay: stockPay > 0 ? stockPay : 0, // Set to null if stock is 0
                stockPeat: stockPeat > 0 ? stockPeat : 0, // Set to null if stock is 0
                stockSal: stockSal > 0 ? stockSal : 0 // Set to null if stock is 0
                );

                SubProductDto existingSubproduct = GetExistingSub(sub);

                if (existingSubproduct == null) //si no existe se agrega
                {
                    SubProductDto newSub = AddSubProduct(sub);
                    SubProductDto subProductoConStocks = SubproductMapper.MapStocksToSubProductDto(newSub, stockPdelE, stockCol, stockPay, stockPeat, stockSal);
                    UpdateStocks(subProductoConStocks);
                    EntryDto entry = new EntryDto(
                        Id: 0,
                        Description: $"Subproducto agregado: {newSub.productCode} - {newSub.Color} - {newSub.Size}",
                        Date: DateTime.Now,
                        Type: "Success",
                        Status: "Ok",
                        ErrorMessage: null
                    );
                    return newSub;
                }
                else //si ya existe se actualizan los stocks solo
                {
                    SubProductDto subProductoConStocks = SubproductMapper.MapStocksToSubProductDto(existingSubproduct, stockPdelE, stockCol, stockPay, stockPeat, stockSal);
                    UpdateStocks(subProductoConStocks);
                    EntryDto entry = new EntryDto(
                        Id: 0,
                        Description: $"Subproducto existente actualizado: {existingSubproduct.productCode} - {existingSubproduct.Color} - {existingSubproduct.Size}",
                        Date: DateTime.Now,
                        Type: "Success",
                        Status: "Ok",
                        ErrorMessage: null
                    );
                    return existingSubproduct;
                }
            }
            }
            catch (Exception ex)
            {
                EntryDto entry = new EntryDto(
                    Id: 0,
                    Description: "Error al procesar la línea",
                    Date: DateTime.Now,
                    Type: "Error",
                    Status: "Fallido",
                    ErrorMessage: ex.Message
                );
                entries.Add(entry);
                Console.WriteLine($"Error al convertir la línea: {ex.Message}");
            }
            return null;
        }

        private static SubProductDto? GetExistingSub(SubProductDto sub)
        {
            try
            {
                // Suponiendo que productsCalls.getSubProductsByProductCode devuelve una lista de SubProductDto
                var existingSubs = productsCalls.getSubProductsByProductCode(sub.productCode);

                if (existingSubs != null)
                {
                    var match = existingSubs.FirstOrDefault(x =>
                        x.Color.Equals(sub.Color, StringComparison.OrdinalIgnoreCase) &&
                        x.Size.Equals(sub.Size, StringComparison.OrdinalIgnoreCase));
                    if (match != null)
                    {
                        Console.WriteLine($"El subproducto ya existe: {match.Color} - {match.Size}");
                        return match; // Retorna el subproducto existente
                    }
                }
                return null; // No existe el subproducto
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar la existencia del subproducto: {ex.Message}");
                return null; // Return false in case of an error
            }
        }

        private static SubProductDto AddSubProduct(SubProductDto sub)
        {
            try
            {
                int id = int.Parse( productsCalls.AddSubProduct(sub));
                if (id != null)
                {
                    // Corrected the return type of productsCalls.getSubById to match the expected SubProductDto
                    SubProductDto subActualizado =  productsCalls.getSubById(id); //Acá está el problema
                    Console.WriteLine($"Subproducto agregado: {subActualizado}");
                    
                    return subActualizado; // Return the subproduct if added successfully

                }
                else
                {
                    Console.WriteLine("No se pudo agregar el subproducto.");
                    return null; // Return null if the addition failed
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el subproducto: {ex.Message}");
                return null; // Return null in case of an error
            }
        }

        private static void UpdateStocks(SubProductDto subProductDto)
        {
            try
            {
             productsCalls.SetStocks(subProductDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar los stocks: {ex.Message}");
            }
        }
    }
}
