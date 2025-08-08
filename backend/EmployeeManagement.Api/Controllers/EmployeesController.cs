using EmployeeManagement.Api.Data;
using EmployeeManagement.Api.Dtos;
using EmployeeManagement.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<EmployeeDto>>> GetAll([FromQuery] EmployeeQueryParams query)
        {
            var queryable = _context.Employees
                .Include(e => e.Manager)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                queryable = queryable.Where(e =>
                    e.FullName.Contains(query.Search) ||
                    e.Email.Contains(query.Search) ||
                    e.Position.Contains(query.Search));
            }

            if (!string.IsNullOrWhiteSpace(query.Position))
            {
                queryable = queryable.Where(e => e.Position == query.Position);
            }

            var total = await queryable.CountAsync();

            var employees = await queryable
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    Email = e.Email,
                    BirthDate = e.BirthDate,
                    DocNumber = e.DocNumber,
                    Phones = e.Phones,
                    Position = e.Position,
                    Salary = e.Salary,
                    ManagerName = e.Manager != null ? e.Manager.FullName : null
                })
                .ToListAsync();

            var result = new PagedResult<EmployeeDto>
            {
                TotalCount = total,
                Items = employees
            };

            return Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateEmployeeDto dto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee is null) return NotFound();

            if (dto.FullName is not null) employee.FullName = dto.FullName;
            if (dto.Email is not null) employee.Email = dto.Email;
            if (dto.BirthDate is not null) employee.BirthDate = dto.BirthDate.Value;
            if (dto.DocNumber is not null) employee.DocNumber = dto.DocNumber;
            if (dto.Phones is not null) employee.Phones = dto.Phones;
            if (dto.Position is not null) employee.Position = dto.Position;
            if (dto.Salary is not null) employee.Salary = dto.Salary.Value;
            employee.ManagerId = dto.ManagerId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto dto)
        {
            var userPosition = User.FindFirst("position")?.Value;

            if (userPosition is null)
                return Unauthorized("Usuário sem cargo definido no token.");

            if (!Enum.TryParse<PositionLevel>(userPosition, true, out var userLevel))
                return BadRequest("Cargo do usuário inválido.");

            if (!Enum.TryParse<PositionLevel>(dto.Position, true, out var newEmployeeLevel))
                return BadRequest("Cargo do novo funcionário inválido.");

            if (newEmployeeLevel > userLevel)
                return Forbid("Você não tem permissão para criar um funcionário com cargo superior ao seu.");

            var employee = new Employee
            {
                FullName = dto.FullName,
                Email = dto.Email,
                BirthDate = dto.BirthDate,
                DocNumber = dto.DocNumber,
                Phones = dto.Phones,
                Position = dto.Position,
                Salary = dto.Salary,
                ManagerId = dto.ManagerId
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpGet("manager/{managerId}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetByManager(int managerId)
        {
            var manager = await _context.Employees.FindAsync(managerId);
            if (manager is null) return NotFound("Not Found.");

            var subordinates = await _context.Employees
                .Where(e => e.ManagerId == managerId)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    Email = e.Email,
                    BirthDate = e.BirthDate,
                    DocNumber = e.DocNumber,
                    Phones = e.Phones,
                    Position = e.Position,
                    Salary = e.Salary,
                    ManagerName = manager.FullName
                })
                .ToListAsync();

            return Ok(subordinates);
        }
        
        [HttpGet("summary/positions")]
        public async Task<ActionResult<IEnumerable<PositionSummaryDto>>> GetPositionSummary()
        {
            var summary = await _context.Employees
                .GroupBy(e => e.Position)
               .Select(g => new PositionSummaryDto(
                    g.Key,
                    g.Count()
                ))
                .ToListAsync();

            return Ok(summary);
        }

    }
}
