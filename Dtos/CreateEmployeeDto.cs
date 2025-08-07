namespace EmployeeManagement.Api.Dtos;

// payload para criar
public record CreateEmployeeDto(
    string FullName,
    string Email,
    DateTime BirthDate,
    string DocNumber,
    List<string> Phones,
    string Position,
    int? ManagerId);

// payload para atualizar
public record UpdateEmployeeDto(
    string? FullName,
    string? Email,
    DateTime? BirthDate,
    string? DocNumber,
    List<string>? Phones,
    string? Position,
    int? ManagerId);
