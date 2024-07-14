using Microsoft.AspNetCore.Mvc;
using MyPokedexAPI.Data;
using MyPokedexAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MyPokedexAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("CreateOrUpdateRole")]
        public async Task<IActionResult> CreateOrUpdateRole([FromBody] RoleDTO roleDto)
        {
            if (roleDto == null)
            {
                return BadRequest();
            }

            if (roleDto.CreatedBy.HasValue)
            {
                var createdByUser = await _context.Users.FindAsync(roleDto.CreatedBy.Value);
                if (createdByUser == null)
                {
                    return BadRequest("CreatedBy user not found.");
                }
            }

            if (roleDto.UpdatedBy.HasValue)
            {
                var updatedByUser = await _context.Users.FindAsync(roleDto.UpdatedBy.Value);
                if (updatedByUser == null)
                {
                    return BadRequest("UpdatedBy user not found.");
                }
            }

            var role = new Role
            {
                Id = roleDto.Id,
                Name = roleDto.Name,
                CreatedBy = roleDto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                UpdatedBy = roleDto.UpdatedBy,
                UpdatedOn = roleDto.UpdatedOn
            };

            if (role.Id == 0)
            {
                await _context.Roles.AddAsync(role);
            }
            else
            {
                var existingRole = await _context.Roles.FindAsync(role.Id);
                if (existingRole == null)
                {
                    return NotFound();
                }

                existingRole.Name = role.Name;
                existingRole.UpdatedBy = roleDto.UpdatedBy;
                existingRole.UpdatedOn = DateTime.UtcNow;

                _context.Roles.Update(existingRole);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return Ok(roleDto);
        }
    }
}
