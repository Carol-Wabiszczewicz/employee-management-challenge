using FluentValidation.TestHelper;
using EmployeeManagement.Api.Validators;
using EmployeeManagement.Api.Dtos;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Api.Data;
namespace EmployeeManagement.Api.Tests
{
    public class CreateEmployeeDtoValidatorTests
    {
        private readonly CreateEmployeeDtoValidator _validator;

        public CreateEmployeeDtoValidatorTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var context = new AppDbContext(options);
            _validator = new CreateEmployeeDtoValidator(context);
        }

        [Fact]
        public async Task Should_Have_Error_When_FullName_Is_Empty()
        {
            var model = new CreateEmployeeDto(
                "",
                "alice@example.com",
                DateTime.Parse("1990-02-05"),
                "123456789",
                new List<string> { "11 98765-4321" },
                "Developer",
                12000.00m,
                1
            );

            var result = await _validator.TestValidateAsync(model);
            result.ShouldHaveValidationErrorFor(x => x.FullName);
        }

        [Fact]
        public async Task Should_Not_Have_Error_When_All_Fields_Are_Valid()
        {
            var model = new CreateEmployeeDto(
                "Alice Souza",
                "alice@example.com",
                DateTime.Parse("1990-02-05"),
                "123456789",
                new List<string> { "11 98765-4321" },
                "Developer",
                12000.00m,
                1
            );

            var result = await _validator.TestValidateAsync(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
