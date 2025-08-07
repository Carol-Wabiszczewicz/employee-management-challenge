namespace EmployeeManagement.Api.Models
{
    public class Employee
    {
        public int Id            { get; set; }

        // Dados principais
        public string FullName   { get; set; } = null!;
        public string Email      { get; set; } = null!;
        public DateTime BirthDate{ get; set; }

        // Identificação / contato
        public string DocNumber  { get; set; } = null!;      
        public List<string> Phones { get; set; } = [];       

        // Estrutura organizacional
        public string Position   { get; set; } = null!;
        public decimal Salary    { get; set; }

        // Relação hierárquica simples
        public int? ManagerId    { get; set; }
        public Employee? Manager { get; set; }
    }
}
