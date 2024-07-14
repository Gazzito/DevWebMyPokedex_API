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
    public class RolesController : ControllerBase  // Define a classe RolesController que herda de ControllerBase
    {
        private readonly ApplicationDbContext _context;  // Campo para o contexto da base de dados

        public RolesController(ApplicationDbContext context)  // Construtor que inicializa o campo _context
        {
            _context = context;
        }

        [HttpPost("CreateOrUpdateRole")]  // Define um endpoint HTTP POST na rota "CreateOrUpdateRole"
        public async Task<IActionResult> CreateOrUpdateRole([FromBody] RoleDTO roleDto)  // Método para criar ou atualizar uma role
        {
            if (roleDto == null)  // Verifica se o DTO da role é nulo
            {
                return BadRequest();  // Retorna um erro de pedido inválido
            }

            if (roleDto.CreatedBy.HasValue)  // Se o campo CreatedBy tiver um valor
            {
                var createdByUser = await _context.Users.FindAsync(roleDto.CreatedBy.Value);  // Procura o utilizador que criou
                if (createdByUser == null)  // Se o utilizador não for encontrado
                {
                    return BadRequest("CreatedBy user not found.");  // Retorna um erro de pedido inválido
                }
            }

            if (roleDto.UpdatedBy.HasValue)  // Se o campo UpdatedBy tiver um valor
            {
                var updatedByUser = await _context.Users.FindAsync(roleDto.UpdatedBy.Value);  // Procura o utilizador que atualizou
                if (updatedByUser == null)  // Se o utilizador não for encontrado
                {
                    return BadRequest("UpdatedBy user not found.");  // Retorna um erro de pedido inválido
                }
            }

            var role = new Role  // Cria uma nova instância de Role
            {
                Id = roleDto.Id,
                Name = roleDto.Name,
                CreatedBy = roleDto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                UpdatedBy = roleDto.UpdatedBy,
                UpdatedOn = roleDto.UpdatedOn
            };

            if (role.Id == 0)  // Se o ID da role for zero, cria uma nova role
            {
                await _context.Roles.AddAsync(role);  // Adiciona a nova role ao contexto
            }
            else  // Se o ID da role não for zero, atualiza uma role existente
            {
                var existingRole = await _context.Roles.FindAsync(role.Id);  // Procura a role existente na base de dados
                if (existingRole == null)  // Se a role não for encontrada
                {
                    return NotFound();  // Retorna um erro de não encontrado
                }

                existingRole.Name = role.Name;
                existingRole.UpdatedBy = roleDto.UpdatedBy;
                existingRole.UpdatedOn = DateTime.UtcNow;

                _context.Roles.Update(existingRole);  // Atualiza a role no contexto
            }

            try
            {
                await _context.SaveChangesAsync();  // Salva as alterações na base de dados
            }
            catch (DbUpdateException ex)  // Captura exceções de atualização da base de dados
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");  // Retorna um erro de servidor interno
            }

            return Ok(roleDto);  // Retorna o DTO da role
        }
    }
}
