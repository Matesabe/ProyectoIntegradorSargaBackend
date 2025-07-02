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

        public static async Task ClearStocks()
        {
            using var client = new HttpClient();
            var url = "http://localhost:5246/api/warehouses/clear-stocks";
            var response = await client.PutAsync(url, null);
            if (!response.IsSuccessStatusCode)
            {
                // Maneja el error según sea necesario
                throw new Exception($"Error al limpiar los stocks: {response.StatusCode}");
            }
        }

        public static async Task<string> AddSubProductAsync(SubProductDto sub)
        {
            using var client = new HttpClient();
            var url = "http://localhost:5246/api/subproducts"; // Verifica que sea correcto
            var json = JsonSerializer.Serialize(sub);

            string contentString = FlattenSubProductDto(sub); // Utiliza el método para aplanar el DTO
            var content = new StringContent(contentString, Encoding.UTF8, "application/json"); 

            // Cambia PutAsync por PostAsync para probar si el endpoint lo requiere
            var response = await client.PostAsync(url, content);

            // Para depuración, puedes leer el contenido de la respuesta
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al agregar el subproducto: {response.StatusCode} - {responseBody}");
            }
            return responseBody; // Devuelve la respuesta de la API
        }

        // Plan en pseudocódigo:
        // 1. Crear un método que reciba un SubProductDto y devuelva un diccionario plano (Dictionary<string, object>)
        // 2. Extraer las propiedades de SubProductDto y agregarlas al diccionario con clave/valor simple
        // 3. Si hay listas (como Images), serializarlas a JSON string o ignorarlas según necesidad
        // 4. Serializar el diccionario plano a JSON antes de enviarlo en la petición
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

            // Si necesitas incluir Images como string JSON:
            if (sub.Images != null)
                dict["Images"] = JsonSerializer.Serialize(sub.Images);

            return JsonSerializer.Serialize(dict);
        }

        public static async Task ClearSubProducts()
        {
            using var client = new HttpClient();
            var url = "http://localhost:5246/api/subproducts/clear";
            var response = await client.DeleteAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                // Maneja el error según sea necesario
                throw new Exception($"Error al limpiar los subproductos: {response.StatusCode}");
            }
        }

        public static async Task SetStocksAsync(SubProductDto sub)
        {
                using var client = new HttpClient();
                var url = "http://localhost:5246/api/warehouses/update-stocks"; 

                var json = JsonSerializer.Serialize(sub);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Status: {response.StatusCode}");
            Console.WriteLine($"Body: {responseBody}");


            if (!response.IsSuccessStatusCode)
                {
                    // Maneja el error según sea necesario
                    throw new Exception($"Error al actualizar stocks: {response.StatusCode}");
                }
        }

        public static async Task<SubProductDto?> getSubById(int id)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"http://localhost:5246/api/subproducts?Id={id}";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error al obtener el subproducto por ID: {response.StatusCode}");
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                var subProducts = JsonSerializer.Deserialize<List<SubProductDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return subProducts?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el subproducto por ID: {ex.Message}");
                return null;
            }
        }

    }
}
