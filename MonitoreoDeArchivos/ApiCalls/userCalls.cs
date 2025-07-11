using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MonitoreoDeArchivos.ApiCalls
{
    public class userCalls
    {
        public static async Task<UserDto?> GetClientByCi(string ci)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"http://localhost:5246/api/users/ci/{ci}";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error al obtener el usuario por CI: {response.StatusCode}");
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                UserDto user = JsonSerializer.Deserialize<UserDto>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el usuario por CI: {ex.Message}");
                return null;
            }
        }
    }
}
