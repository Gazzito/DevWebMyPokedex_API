using Microsoft.AspNetCore.Mvc;
using MyPokedexAPI.Data;
using MyPokedexAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;

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
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("CreateOrUpdateUser")]
        public async Task<IActionResult> CreateOrUpdateUser([FromBody] UserDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest();
            }

            var user = new User
            {
                Id = userDto.Id,
                Name = userDto.Name,
                Username = userDto.Username,
                Password = userDto.Password,
                Email = userDto.Email,
                MobilePhone = userDto.MobilePhone,
                Creation_Date = DateTime.SpecifyKind(userDto.Creation_Date, DateTimeKind.Utc),
                Last_Login = DateTime.SpecifyKind(userDto.Last_Login, DateTimeKind.Utc),
                Is_Active = userDto.Is_Active
            };

            if (user.Id == 0)
            {
                await _context.Users.AddAsync(user);
            }
            else
            {
                var existingUser = await _context.Users.FindAsync(user.Id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                existingUser.Name = user.Name;
                existingUser.Username = user.Username;
                existingUser.Password = user.Password;
                existingUser.Email = user.Email;
                existingUser.MobilePhone = user.MobilePhone;
                existingUser.Creation_Date = user.Creation_Date;
                existingUser.Last_Login = user.Last_Login;
                existingUser.Is_Active = user.Is_Active;

                _context.Users.Update(existingUser);
            }

            await _context.SaveChangesAsync();
            return Ok(user);
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
