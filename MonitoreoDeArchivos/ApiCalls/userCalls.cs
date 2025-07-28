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
        public static UserDto? GetClientByCi(string ci)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"http://localhost:5246/api/users/ci/{ci}";

                var response = client.GetAsync(url).Result; // Bloquea hasta que se reciba la respuesta

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error al obtener el usuario por CI: {response.StatusCode}");
                    return null;
                }

                var json = response.Content.ReadAsStringAsync().Result; // Bloquea para leer contenido

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
