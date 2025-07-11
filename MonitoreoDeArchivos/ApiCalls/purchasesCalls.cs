using AppLogic.UseCase.PurchaseUC;
using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MonitoreoDeArchivos.ApiCalls
{
    public class purchasesCalls{
        public static async Task ClearPurchases(){
            using var client = new HttpClient();
            var url = "http://localhost:5246/api/purchases/clear";
            var response = await client.DeleteAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                // Maneja el error según sea necesario
                throw new Exception($"Error al limpiar las ventas: {response.StatusCode}");
            }
        }

        public static async Task<List<PurchaseDto>?> GetAllPurchases()
        {
            try
            {
                using var client = new HttpClient();
                var url = "http://localhost:5246/api/purchases";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al obtener las compras: {response.StatusCode}");
                }
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<PurchaseDto>>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la llamada a la API: {ex.Message}");
                return null;
            }
        }

        public static async void updatePurchase(PurchaseDto updatedPurchase)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"http://localhost:5246/api/purchases/{updatedPurchase.Id}";
                var json = JsonSerializer.Serialize(updatedPurchase);
                string contentString = FlattenPurchaseDto(updatedPurchase); // Utiliza el método para aplanar el DTO
                var content = new StringContent(contentString, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(url, content);
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al actualizar la compra: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la llamada a la API: {ex.Message}");
            }
        }

        public static string FlattenPurchaseDto(PurchaseDto pur)
        {
            var dict = new Dictionary<string, object>
            {
                { "Id", pur.Id },
                { "ClientId", pur.Client.Id },
                { "Amount", pur.Amount },
                { "PointsGenerated", pur.PointsGenerated },
            };
            // Aplanar la lista de productos
            var products = pur.Products.Select(p => new
            {
                Id = p.id, 
                Name = p.name,
                Price = p.price,
            }).ToList();
            dict.Add("Products", products);
            return JsonSerializer.Serialize(dict);
        }

        public static async Task<string> AddSubPurchaseAsync(PurchaseDto pur)
        {
            using var client = new HttpClient();
            var url = "http://localhost:5246/api/purchases"; // Verifica que sea correcto
            var json = JsonSerializer.Serialize(pur);

            /*string contentString = FlattenPurchaseDto(pur); */// Utiliza el método para aplanar el DTO
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);

            // Para depuración, puedes leer el contenido de la respuesta
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al agregar la venta: {response.StatusCode} - {responseBody}");
            }
            return responseBody; // Devuelve la respuesta de la API
        }

        public static async Task<PurchaseDto> getPurchaseById(int id)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"http://localhost:5246/api/purchases/{id}";
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al obtener la compra por ID: {response.StatusCode}");
                }
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<PurchaseDto>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la llamada a la API: {ex.Message}");
                return null;
            }
        }
    }
}
