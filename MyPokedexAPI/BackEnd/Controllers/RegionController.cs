using Microsoft.AspNetCore.Mvc;  // Importa o namespace para funcionalidades do MVC no ASP.NET Core
using MyPokedexAPI.Data;  // Importa o namespace para acesso ao contexto da base de dados
using MyPokedexAPI.Models;  // Importa o namespace para os modelos da aplicação
using Microsoft.EntityFrameworkCore;  // Importa o namespace para funcionalidades do Entity Framework Core
using System.Linq;  // Importa o namespace para funcionalidades de consultas LINQ
using System.Threading.Tasks;  // Importa o namespace para funcionalidades assíncronas
using System;  // Importa o namespace para funcionalidades básicas do sistema
using Microsoft.AspNetCore.Authorization;  // Importa o namespace para funcionalidades de autorização

namespace MyPokedexAPI.Controllers  // Define o namespace para o controlador da API
{
    [Authorize]  // Indica que este controlador requer autorização
    [ApiController]  // Indica que esta classe é um controlador de API
    [Route("api/[controller]")]  // Define a rota para aceder a este controlador
    public class RegionsController : ControllerBase  // Define a classe RegionsController que herda de ControllerBase
    {
        private readonly ApplicationDbContext _context;  // Campo para o contexto da base de dados

        public RegionsController(ApplicationDbContext context)  // Construtor que inicializa o campo _context
        {
            _context = context;
        }

        [HttpGet("GetAllRegions")]  // Define um endpoint HTTP GET na rota "GetAllRegions"
        public async Task<IActionResult> GetAllRegions()  // Método para obter todas as regiões
        {
            var regions = await _context.Regions  // Consulta para obter todas as regiões
                .Select(r => new RegionDTO  // Projeta os resultados para o DTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    CreatedOn = r.CreatedOn,
                    CreatedBy = r.CreatedBy,
                    UpdatedOn = r.UpdatedOn,
                    UpdatedBy = r.UpdatedBy
                })
                .ToListAsync();  // Converte o resultado para uma lista de forma assíncrona

            return Ok(regions);  // Retorna todas as regiões
        }

        [HttpPost("CreateOrUpdateRegion")]  // Define um endpoint HTTP POST na rota "CreateOrUpdateRegion"
        public async Task<IActionResult> CreateOrUpdateRegion([FromBody] RegionDTO regionDto)  // Método para criar ou atualizar uma região
        {
            if (regionDto == null)  // Verifica se o DTO da região é nulo
            {
                return BadRequest();  // Retorna um erro de pedido inválido
            }

            Region region;
            if (regionDto.Id == 0)  // Se o ID da região for zero, cria uma nova região
            {
                region = new Region
                {
                    Name = regionDto.Name,
                    CreatedOn = DateTime.SpecifyKind(regionDto.CreatedOn, DateTimeKind.Utc),
                    CreatedBy = regionDto.CreatedBy,
                    UpdatedOn = regionDto.UpdatedOn.HasValue ? DateTime.SpecifyKind(regionDto.UpdatedOn.Value, DateTimeKind.Utc) : (DateTime?)null,
                    UpdatedBy = regionDto.UpdatedBy
                };

                await _context.Regions.AddAsync(region);  // Adiciona a nova região ao contexto
                await _context.SaveChangesAsync();  // Salva as alterações na base de dados

                regionDto.Id = region.Id;  // Atualiza o ID no DTO
            }
            else  // Se o ID da região não for zero, atualiza uma região existente
            {
                region = await _context.Regions.FindAsync(regionDto.Id);  // Procura a região na base de dados
                if (region == null)  // Se a região não for encontrada
                {
                    return NotFound();  // Retorna um erro de não encontrado
                }

                region.Name = regionDto.Name;
                region.CreatedOn = regionDto.CreatedOn;
                region.CreatedBy = regionDto.CreatedBy;
                region.UpdatedOn = regionDto.UpdatedOn;
                region.UpdatedBy = regionDto.UpdatedBy;

                _context.Regions.Update(region);  // Atualiza a região no contexto
                await _context.SaveChangesAsync();  // Salva as alterações na base de dados
            }

            // Retorna o DTO atualizado com o ID da nova região criada
            regionDto.Id = region.Id;
            return Ok(regionDto);
        }

        [HttpDelete("DeleteRegion/{id}")]  // Define um endpoint HTTP DELETE na rota "DeleteRegion/{id}"
        public async Task<IActionResult> DeleteRegion(int id)  // Método para deletar uma região
        {
            var region = await _context.Regions.FindAsync(id);  // Procura a região na base de dados
            if (region == null)  // Se a região não for encontrada
            {
                return NotFound();  // Retorna um erro de não encontrado
            }

            _context.Regions.Remove(region);  // Remove a região do contexto
            await _context.SaveChangesAsync();  // Salva as alterações na base de dados

            return Ok();  // Retorna uma resposta de sucesso
        }
    }
}
