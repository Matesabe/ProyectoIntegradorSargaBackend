using SharedUseCase.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text;


namespace MonitoreoDeArchivos.ApiCalls
{
    public class productsCalls
    {
        public static void ClearStocks()
        {
            using var client = new HttpClient();
            var url = "http://localhost:5246/api/warehouses/clear-stocks";
            var response = client.PutAsync(url, null).GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al limpiar los stocks: {response.StatusCode}");
            }
        }

        public static string AddSubProduct(SubProductDto sub)
        {
            using var client = new HttpClient();
            var url = "http://localhost:5246/api/subproducts";
            string contentString = FlattenSubProductDto(sub);
            var content = new StringContent(contentString, Encoding.UTF8, "application/json");
            var response = client.PostAsync(url, content).GetAwaiter().GetResult();
            var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al agregar el subproducto: {response.StatusCode} - {responseBody}");
            }
            return responseBody;
        }

        public static string FlattenSubProductDto(SubProductDto sub)
        {
            var dict = new Dictionary<string, object>
            {
                ["Id"] = sub.Id,
                ["ProductId"] = sub.ProductId,
                ["productCode"] = sub.productCode,
                ["Name"] = sub.Name,
                ["Price"] = sub.Price,
                ["Color"] = sub.Color,
                ["Size"] = sub.Size,
                ["Season"] = sub.Season,
                ["Year"] = sub.Year,
                ["genre"] = sub.genre,
                ["brand"] = sub.brand,
                ["type"] = sub.type,
                ["stockPdelE"] = sub.stockPdelE,
                ["stockCol"] = sub.stockCol,
                ["stockPay"] = sub.stockPay,
                ["stockPeat"] = sub.stockPeat,
                ["stockSal"] = sub.stockSal
            };

            if (sub.Images != null)
                dict["Images"] = JsonSerializer.Serialize(sub.Images);

            return JsonSerializer.Serialize(dict);
        }

        public static void ClearSubProducts()
        {
            using var client = new HttpClient();
            var url = "http://localhost:5246/api/subproducts/clear";
            var response = client.DeleteAsync(url).GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al limpiar los subproductos: {response.StatusCode}");
            }
        }

        public static void SetStocks(SubProductDto sub)
        {
            using var client = new HttpClient();
            var url = "http://localhost:5246/api/warehouses/update-stocks";
            var json = JsonSerializer.Serialize(sub);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = client.PutAsync(url, content).GetAwaiter().GetResult();
            var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Console.WriteLine($"Status: {response.StatusCode}");
            Console.WriteLine($"Body: {responseBody}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al actualizar stocks: {response.StatusCode}");
            }
        }

        public static SubProductDto? getSubById(int id)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"http://localhost:5246/api/subproducts/{id}";
                var response = client.GetAsync(url).GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error al obtener el subproducto por ID: {response.StatusCode}");
                    return null;
                }
                var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var subProduct = JsonSerializer.Deserialize<SubProductDto>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return subProduct;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el subproducto por ID: {ex.Message}");
                return null;
            }
        }

        public static List<SubProductDto> getSubProductsByProductCode(string productCode)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"http://localhost:5246/api/subproducts/byProductCode/{productCode}";
                var response = client.GetAsync(url).GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error al obtener los subproductos por código de producto: {response.StatusCode}");
                    return new List<SubProductDto>();
                }
                var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var subProducts = JsonSerializer.Deserialize<List<SubProductDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return subProducts ?? new List<SubProductDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los subproductos por código de producto: {ex.Message}");
                return new List<SubProductDto>();
            }
        }

        public static ProductDto? GetProductByCode(string v)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"http://localhost:5246/api/SubProducts/products/{v}";
                var response = client.GetAsync(url).GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error al obtener el producto por código: {response.StatusCode}");
                    return null;
                }
                var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var product = JsonSerializer.Deserialize<List<ProductDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (product == null || product.Count == 0)
                {
                    Console.WriteLine("No se encontró ningún producto con el código especificado.");
                    return null;
                }
                return product.First();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el producto por código: {ex.Message}");
                return null;
            }
        }

        public static void DeleteSubProduct(int id)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"http://localhost:5246/api/subproducts/{id}";
                var response = client.DeleteAsync(url).GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error al eliminar el subproducto: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el subproducto: {ex.Message}");
            }
        }
    }
}
