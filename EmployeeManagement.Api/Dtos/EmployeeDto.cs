namespace EmployeeManagement.Api.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string DocNumber { get; set; } = string.Empty;
        public List<string> Phones { get; set; } = new();
        public string Position { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string? ManagerName { get; set; }
    }
}
