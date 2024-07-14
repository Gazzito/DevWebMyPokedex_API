using Microsoft.AspNetCore.Mvc;  // Importa o namespace para funcionalidades do MVC no ASP.NET Core
using MyPokedexAPI.Data;  // Importa o namespace para acesso ao contexto da base de dados
using MyPokedexAPI.Models;  // Importa o namespace para os modelos da aplicação
using System.Threading.Tasks;  // Importa o namespace para funcionalidades assíncronas
using Microsoft.EntityFrameworkCore;  // Importa o namespace para funcionalidades do Entity Framework Core
using Microsoft.AspNetCore.Authorization;  // Importa o namespace para funcionalidades de autorização

namespace MyPokedexAPI.Controllers  // Define o namespace para o controlador da API
{
    [Authorize]  // Indica que este controlador requer autorização
    [ApiController]  // Indica que esta classe é um controlador de API
    [Route("api/[controller]")]  // Define a rota para aceder a este controlador
    public class UserRolesController : ControllerBase  // Define a classe UserRolesController que herda de ControllerBase
    {
        private readonly ApplicationDbContext _context;  // Campo para o contexto da base de dados

        public UserRolesController(ApplicationDbContext context)  // Construtor que inicializa o campo _context
        {
            _context = context;
        }

        [HttpPost("CreateUserRole")]  // Define um endpoint HTTP POST na rota "CreateUserRole"
        public async Task<IActionResult> CreateUserRole([FromBody] UserRoleDTO userRoleDto)  // Método para criar uma associação entre utilizador e role
        {
            if (userRoleDto == null)  // Verifica se o DTO da associação é nulo
            {
                return BadRequest();  // Retorna um erro de pedido inválido
            }

            // Verifica se o UserId é válido
            var user = await _context.Users.FindAsync(userRoleDto.UserId);  // Procura o utilizador na base de dados
            if (user == null)  // Se o utilizador não for encontrado
            {
                return BadRequest("User not found.");  // Retorna um erro de pedido inválido
            }

            // Verifica se o RoleId é válido
            var role = await _context.Roles.FindAsync(userRoleDto.RoleId);  // Procura a role na base de dados
            if (role == null)  // Se a role não for encontrada
            {
                return BadRequest("Role not found.");  // Retorna um erro de pedido inválido
            }

            var userRole = new UserRole  // Cria uma nova instância de UserRole
            {
                UserId = userRoleDto.UserId,
                RoleId = userRoleDto.RoleId
            };

            await _context.UserRoles.AddAsync(userRole);  // Adiciona a nova associação ao contexto
            await _context.SaveChangesAsync();  // Salva as alterações na base de dados

            return Ok(userRoleDto);  // Retorna o DTO da associação criada
        }
    }
}
