using Microsoft.AspNetCore.Mvc;  // Importa o namespace para funcionalidades do MVC no ASP.NET Core
using MyPokedexAPI.Data;  // Importa o namespace para acesso ao contexto da base de dados
using MyPokedexAPI.Models;  // Importa o namespace para os modelos da aplicação
using Microsoft.EntityFrameworkCore;  // Importa o namespace para funcionalidades do Entity Framework Core
using System.Linq;  // Importa o namespace para funcionalidades de consultas LINQ
using System.Threading.Tasks;  // Importa o namespace para funcionalidades assíncronas
using Microsoft.AspNetCore.Authorization;  // Importa o namespace para funcionalidades de autorização

namespace MyPokedexAPI.Controllers  // Define o namespace para o controlador da API
{
    [Authorize]  // Indica que este controlador requer autorização
    [ApiController]  // Indica que esta classe é um controlador de API
    [Route("api/[controller]")]  // Define a rota para aceder a este controlador
    public class UsersController : ControllerBase  // Define a classe UsersController que herda de ControllerBase
    {
        private readonly ApplicationDbContext _context;  // Campo para o contexto da base de dados

        public UsersController(ApplicationDbContext context)  // Construtor que inicializa o campo _context
        {
            _context = context;
        }

        [HttpGet("GetAllUsers")]  // Define um endpoint HTTP GET na rota "GetAllUsers"
        public async Task<IActionResult> GetAllUsers()  // Método para obter todos os utilizadores
        {
            var users = await _context.Users  // Consulta para obter todos os utilizadores
                .Select(u => new UserDTO  // Projeta os resultados para o DTO
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
                .ToListAsync();  // Converte o resultado para uma lista de forma assíncrona

            return Ok(users);  // Retorna todos os utilizadores
        }

        [HttpPost("CreateOrUpdateUser")]  // Define um endpoint HTTP POST na rota "CreateOrUpdateUser"
        public async Task<IActionResult> CreateOrUpdateUser([FromBody] UserDTO userDto)  // Método para criar ou atualizar um utilizador
        {
            if (userDto == null)  // Verifica se o DTO do utilizador é nulo
            {
                return BadRequest();  // Retorna um erro de pedido inválido
            }

            User user;
            if (userDto.Id == 0)  // Se o ID do utilizador for zero, cria um novo utilizador
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

                await _context.Users.AddAsync(user);  // Adiciona o novo utilizador ao contexto
                await _context.SaveChangesAsync();  // Salva as alterações na base de dados

                userDto.Id = user.Id;  // Atualiza o ID no DTO
            }
            else  // Se o ID do utilizador não for zero, atualiza um utilizador existente
            {
                user = await _context.Users.FindAsync(userDto.Id);  // Procura o utilizador na base de dados
                if (user == null)  // Se o utilizador não for encontrado
                {
                    return NotFound();  // Retorna um erro de não encontrado
                }

                user.Name = userDto.Name;
                user.Username = userDto.Username;
                user.Password = userDto.Password;
                user.Email = userDto.Email;
                user.CreationDate = DateTime.SpecifyKind(userDto.CreationDate, DateTimeKind.Utc);
                user.LastLogin = userDto.LastLogin.HasValue ? DateTime.SpecifyKind(userDto.LastLogin.Value, DateTimeKind.Utc) : (DateTime?)null;
                user.IsActive = userDto.IsActive;

                _context.Users.Update(user);  // Atualiza o utilizador no contexto
                await _context.SaveChangesAsync();  // Salva as alterações na base de dados

                userDto.Id = user.Id;  // Atualiza o ID no DTO
            }

            // Retorna o DTO atualizado com o ID do utilizador criado ou atualizado
            return Ok(userDto);
        }

        [HttpDelete("DeleteUser/{id}")]  // Define um endpoint HTTP DELETE na rota "DeleteUser/{id}"
        public async Task<IActionResult> DeleteUser(int id)  // Método para deletar um utilizador
        {
            var user = await _context.Users.FindAsync(id);  // Procura o utilizador na base de dados
            if (user == null)  // Se o utilizador não for encontrado
            {
                return NotFound();  // Retorna um erro de não encontrado
            }

            _context.Users.Remove(user);  // Remove o utilizador do contexto
            await _context.SaveChangesAsync();  // Salva as alterações na base de dados

            return Ok();  // Retorna uma resposta de sucesso
        }

        [HttpGet("GetNextOpenExpected")]  // Define um endpoint HTTP GET na rota "GetNextOpenExpected"
        public async Task<IActionResult> GetNextOpenExpected(int userId)  // Método para obter o próximo pack gratuito esperado para um utilizador
        {
            var user = await _context.Users.FindAsync(userId);  // Procura o utilizador na base de dados
            if (user == null)  // Se o utilizador não for encontrado
            {
                return NotFound("User not found.");  // Retorna um erro de não encontrado
            }

            return Ok(new { NextOpenExpected = user.NextOpenExpected });  // Retorna a próxima abertura esperada
        }
    }
}
