using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using EmployeeManagement.Api.Dtos;

namespace EmployeeManagement.Api.Tests.Integration.Utils
{
    public static class TokenHelper
    {
        public static async Task<string> GetJwtTokenAsync(HttpClient client)
        {
            var loginDto = new LoginDto
            {
                Username = "admin",
                Password = "123"
            };

            var response = await client.PostAsJsonAsync("/api/auth/login", loginDto);
            response.EnsureSuccessStatusCode(); 

            var content = await response.Content.ReadAsStringAsync();

            using var jsonDoc = JsonDocument.Parse(content);
            return jsonDoc.RootElement.GetProperty("token").GetString();
        }
    }
}
