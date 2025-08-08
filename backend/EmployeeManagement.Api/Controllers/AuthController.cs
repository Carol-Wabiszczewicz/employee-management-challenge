using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
       [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
              // Validação fake
            if (dto.Username != "admin" || dto.Password != "123")
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("xR#2sLp8vJz9NcQ4Bh1Tu6EwKfY3MdA0"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "EmployeeManagementAPI",
                audience: "EmployeeManagementUsers",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
