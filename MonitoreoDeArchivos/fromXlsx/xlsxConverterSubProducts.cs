using SharedUseCase.DTOs.Product;
using OfficeOpenXml;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using MonitoreoDeArchivos.ApiCalls;
using System.Text.Json;
using System.Threading.Tasks;
using AppLogic.Mapper;




namespace MonitoreoDeArchivos.fromXlsx
{
    internal class xlsxConverterSubProducts
    {
        public async Task<SubProductDto> ConvertLine(string[] line)
        {
            // Assuming the line contains data in a specific format, e.g.:
            // [0] = ProductId, [1] = Name, [2] = Description, [3] = Price, [4] = Stock
            if (line.Length < 10)
                throw new ArgumentException("Line does not contain enough data to convert to ProductDto.");
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
                stockPdelE: stockPdelE > 0 ? stockPdelE : null, // Set to null if stock is 0
                stockCol: stockCol > 0 ? stockCol : null, // Set to null if stock is 0
                stockPay: stockPay > 0 ? stockPay : null, // Set to null if stock is 0
                stockPeat: stockPeat > 0 ? stockPeat : null, // Set to null if stock is 0
                stockSal: stockSal > 0 ? stockSal : null // Set to null if stock is 0
                );
                SubProductDto newSub = await AddSubProduct(sub);
                SubProductDto updatedSub = SubproductMapper.changeId(newSub, sub);
                UpdateStocks(updatedSub);
                return sub;
            }
            return null;
        }

        public List<SubProductDto> ConvertFromXlsx(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                throw new FileNotFoundException("El archivo especificado no existe.", filePath);
            }

            //Limpiar todos los subproductos de la base de datos antes de agregar nuevos
            try
            {
                productsCalls.ClearSubProducts().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al limpiar los subproductos: {ex.Message}");
            }

            var subProducts = new List<SubProductDto>();
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed().Skip(2); // Salta encabezado
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

                    if (subProducts.Count == 10)
                        break; // límite de 10 para pruebas SACAR DESPUÉS
                }
            }
            return subProducts;
        }
        private async static Task<SubProductDto> AddSubProduct(SubProductDto sub)
        {
            try
            {
                int id = int.Parse(await productsCalls.AddSubProductAsync(sub));
                if (id != null)
                {
                    // Corrected the return type of productsCalls.getSubById to match the expected SubProductDto
                    SubProductDto subActualizado = await productsCalls.getSubById(id);
                    Console.WriteLine($"Subproducto agregado: {sub}");
                    
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
             productsCalls.ClearStocks().Wait();
             productsCalls.SetStocksAsync(subProductDto).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar los stocks: {ex.Message}");
            }
        }
    }
}
