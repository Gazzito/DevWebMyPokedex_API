using Microsoft.AspNetCore.Mvc;
using MyPokedexAPI.Data;
using MyPokedexAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MyPokedexAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Username = u.Username,
                    Password = u.Password,
                    Email = u.Email,
                    CreationDate = u.CreationDate,
                    LastLogin = u.LastLogin,
                    IsActive = u.IsActive
                })
                .ToListAsync();

            return Ok(users);
        }




        [HttpPost("CreateOrUpdateUser")]
public async Task<IActionResult> CreateOrUpdateUser([FromBody] UserDTO userDto)
{
    if (userDto == null)
    {
        return BadRequest();
    }

    User user;
    if (userDto.Id == 0)
    {
        user = new User
        {
            Name = userDto.Name,
            Username = userDto.Username,
            Password = userDto.Password,
            Email = userDto.Email,
            CreationDate = DateTime.SpecifyKind(userDto.CreationDate, DateTimeKind.Utc),
            LastLogin = userDto.LastLogin.HasValue ? DateTime.SpecifyKind(userDto.LastLogin.Value, DateTimeKind.Utc) : (DateTime?)null,
            IsActive = userDto.IsActive,
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        userDto.Id = user.Id;
    }
    else
    {
        user = await _context.Users.FindAsync(userDto.Id);
        if (user == null)
        {
            return NotFound();
        }

        user.Name = userDto.Name;
        user.Username = userDto.Username;
        user.Password = userDto.Password;
        user.Email = userDto.Email;
        user.CreationDate = DateTime.SpecifyKind(userDto.CreationDate, DateTimeKind.Utc);
        user.LastLogin = userDto.LastLogin.HasValue ? DateTime.SpecifyKind(userDto.LastLogin.Value, DateTimeKind.Utc) : (DateTime?)null;
        user.IsActive = userDto.IsActive;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        userDto.Id = user.Id;
    }

    // Retorna o DTO atualizado com o ID do usuário criado ou atualizado
    return Ok(userDto);
}



        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
