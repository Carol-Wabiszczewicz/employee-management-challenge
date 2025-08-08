
using System.Net;
using System.Net.Http.Json;
using EmployeeManagement.Api.Dtos;
using EmployeeManagement.Api.Tests.Integration.Utils;
using Microsoft.AspNetCore.Mvc.Testing;

namespace EmployeeManagement.Api.Tests.Integration
{
    public class EmployeesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private async Task AuthAsync()
        {
            var token = await TokenHelper.GetJwtTokenAsync(_client);
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        [Fact]
        public async Task GetAll_Should_Return_OK()
        {
            await AuthAsync();

            var response = await _client.GetAsync("/api/employees");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public EmployeesControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateEmployee_Should_Return_Created()
        {
            await AuthAsync();

            var dto = new CreateEmployeeDto(
                "João Silva",
                "joao@example.com",
                new DateTime(1990, 5, 10),
                "999999999",
                new List<string> { "11 99999-9999" },
                "Analista",
                9000m,
                null
            );

            var response = await _client.PostAsJsonAsync("/api/employees", dto);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
