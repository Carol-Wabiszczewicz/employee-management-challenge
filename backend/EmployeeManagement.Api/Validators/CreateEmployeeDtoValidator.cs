using EmployeeManagement.Api.Dtos;
using EmployeeManagement.Api.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Validators;

public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
{
    public CreateEmployeeDtoValidator(AppDbContext db)
    {
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(120);

        RuleFor(x => x.BirthDate)
            .Must(d => d <= DateTime.Today.AddYears(-18))
            .WithMessage("Employee must be at least 18 years old.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .MustAsync(async (email, ct) =>
                !await db.Employees.AnyAsync(e => e.Email == email, ct))
            .WithMessage("Email already in use.");

        RuleFor(x => x.DocNumber)
            .NotEmpty()
            .MustAsync(async (doc, ct) =>
                !await db.Employees.AnyAsync(e => e.DocNumber == doc, ct))
            .WithMessage("Doc number must be unique.");

        RuleFor(x => x.Phones)
            .NotEmpty()
            .Must(list => list!.Count >= 1)
            .WithMessage("At least one phone is required.");
    }
}
