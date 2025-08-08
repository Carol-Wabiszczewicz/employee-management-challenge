namespace EmployeeManagement.Api.Models
{
    public class Employee
    {
        public int Id            { get; set; }
        public string FullName   { get; set; } = null!;
        public string Email      { get; set; } = null!;
        public DateTime BirthDate{ get; set; }
        public string DocNumber  { get; set; } = null!;      
        public List<string> Phones { get; set; } = [];       
        public string Position   { get; set; } = null!;
        public decimal Salary    { get; set; }
        public int? ManagerId    { get; set; }
        public Employee? Manager { get; set; }
    }
}
