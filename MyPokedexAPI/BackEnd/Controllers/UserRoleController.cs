using Microsoft.AspNetCore.Mvc;
using MyPokedexAPI.Data;
using MyPokedexAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyPokedexAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserRolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("CreateUserRole")]
        public async Task<IActionResult> CreateUserRole([FromBody] UserRoleDTO userRoleDto)
        {
            if (userRoleDto == null)
            {
                return BadRequest();
            }

            // Verifica se o UserId é válido
            var user = await _context.Users.FindAsync(userRoleDto.UserId);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            // Verifica se o RoleId é válido
            var role = await _context.Roles.FindAsync(userRoleDto.RoleId);
            if (role == null)
            {
                return BadRequest("Role not found.");
            }

            var userRole = new UserRole
            {
                UserId = userRoleDto.UserId,
                RoleId = userRoleDto.RoleId
            };

            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();

            return Ok(userRoleDto);
        }
    }
}
