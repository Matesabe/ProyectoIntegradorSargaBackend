using SharedUseCase.DTOs.Purchase;
using SharedUseCase.DTOs.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MonitoreoDeArchivos.ApiCalls
{
    public class reportsCalls
    {
        public static List<ReportDto>? GetAllPurchases()
        {
            try
            {
                using var client = new HttpClient();
                var url = "http://localhost:5246/api/reports";
                var response = client.GetAsync(url).GetAwaiter().GetResult();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al obtener los reportes: {response.StatusCode}");
                }
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return JsonSerializer.Deserialize<List<ReportDto>>(content, new JsonSerializerOptions
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

        public static string AddReport(ReportDto rep)
        {
            using var client = new HttpClient();
            var url = "http://localhost:5246/api/reports";
            var json = JsonSerializer.Serialize(rep);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync(url, content).GetAwaiter().GetResult();
            var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al agregar el reporte: {response.StatusCode} - {responseBody}");
            }
            return responseBody;
        }
    }
}
