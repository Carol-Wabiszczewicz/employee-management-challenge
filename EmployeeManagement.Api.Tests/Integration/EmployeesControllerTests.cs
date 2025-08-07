
using System.Net;
using System.Net.Http.Json;
using EmployeeManagement.Api.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;

namespace EmployeeManagement.Api.Tests.Integration
{
    public class EmployeesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public EmployeesControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_Should_Return_OK()
        {
            var response = await _client.GetAsync("/api/employees");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateEmployee_Should_Return_Created()
        {
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
