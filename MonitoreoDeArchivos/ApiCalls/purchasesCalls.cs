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
    public class purchasesCalls
    {
        public static void ClearPurchases()
        {
            using var client = new HttpClient();
            var url = "http://localhost:5246/api/purchases/clear";
            var response = client.DeleteAsync(url).GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al limpiar las ventas: {response.StatusCode}");
            }
        }

        public static List<PurchaseDto>? GetAllPurchases()
        {
            try
            {
                using var client = new HttpClient();
                var url = "http://localhost:5246/api/purchases";
                var response = client.GetAsync(url).GetAwaiter().GetResult();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al obtener las compras: {response.StatusCode}");
                }
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return JsonSerializer.Deserialize<List<PurchaseDto>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la llamada a la API: {ex.Message}");
                return null;
            }
        }

        public static void updatePurchase(PurchaseDto updatedPurchase)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"http://localhost:5246/api/purchases/{updatedPurchase.Id}";

                string json = JsonSerializer.Serialize(updatedPurchase, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PutAsync(url, content).GetAwaiter().GetResult();

                if (!response.IsSuccessStatusCode)
                {
                    string errorResponse = response.Content.ReadAsStringAsync().Result;
                    throw new Exception($"Error al actualizar la compra: {response.StatusCode} - {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la llamada a la API: {ex.Message}");
            }
        }

        public static string AddSubPurchase(PurchaseDto pur)
        {
            using var client = new HttpClient();
            var url = "http://localhost:5246/api/purchases";
            var json = JsonSerializer.Serialize(pur);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync(url, content).GetAwaiter().GetResult();
            var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al agregar la venta: {response.StatusCode} - {responseBody}");
            }
            return responseBody;
        }

        public static PurchaseDto? getPurchaseById(int id)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"http://localhost:5246/api/purchases/{id}";
                var response = client.GetAsync(url).GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al obtener la compra por ID: {response.StatusCode}");
                }
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return JsonSerializer.Deserialize<PurchaseDto>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la llamada a la API: {ex.Message}");
                return null;
            }
        }
    }
}
