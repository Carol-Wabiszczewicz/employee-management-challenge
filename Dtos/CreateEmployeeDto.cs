namespace EmployeeManagement.Api.Dtos;

// payload para criar
public record CreateEmployeeDto(
    string FullName,
    string Email,
    DateTime BirthDate,
    string DocNumber,
    List<string> Phones,
    string Position,
    decimal Salary,
    int? ManagerId
);


public record UpdateEmployeeDto(
    string? FullName,
    string? Email,
    DateTime? BirthDate,
    string? DocNumber,
    List<string>? Phones,
    string? Position,
    decimal? Salary,         
    int? ManagerId
);

