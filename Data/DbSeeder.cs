using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api.Data
{
    public static class DbSeeder
    {
       public static void Seed(AppDbContext context)
        {
            if (!context.Employees.Any())
            {
                var bruno = new Employee
                {
                    FullName = "Bruno Lima",
                    Email = "bruno@example.com",
                    DocNumber = "987654321",
                    BirthDate = new DateTime(1985, 8, 19),
                    Phones = new List<string> { "21 99888-1122" },
                    Position = "Manager",
                    Salary = 18000
                };

                var alice = new Employee
                {
                    FullName = "Alice Souza",
                    Email = "alice@example.com",
                    DocNumber = "123456789",
                    BirthDate = new DateTime(1990, 2, 5),
                    Phones = new List<string> { "11 98765-4321" },
                    Position = "Developer",
                    Salary = 12000,
                    Manager = bruno
                };

                var carlos = new Employee
                {
                    FullName = "Carlos Silva",
                    Email = "carlos@example.com",
                    DocNumber = "1122334455",
                    BirthDate = new DateTime(1992, 11, 3),
                    Phones = new List<string> { "31 98888-7766" },
                    Position = "Analyst",
                    Salary = 10000,
                    Manager = bruno
                };

                var daniela = new Employee
                {
                    FullName = "Daniela Costa",
                    Email = "daniela@example.com",
                    DocNumber = "5566778899",
                    BirthDate = new DateTime(1995, 6, 12),
                    Phones = new List<string> { "41 91111-2233" },
                    Position = "Intern",
                    Salary = 5000,
                    // Ela será subordinada de Alice, que é subordinada de Bruno
                    Manager = alice
                };

                context.Employees.AddRange(bruno, alice, carlos, daniela);
                context.SaveChanges();
            }
        }

    }
}
