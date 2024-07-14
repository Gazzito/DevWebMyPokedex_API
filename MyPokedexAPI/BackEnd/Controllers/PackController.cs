using Microsoft.AspNetCore.Mvc;  // Importa o namespace para funcionalidades do MVC no ASP.NET Core
using MyPokedexAPI.Data;  // Importa o namespace para acesso ao contexto da base de dados
using MyPokedexAPI.Models;  // Importa o namespace para os modelos da aplicação
using Microsoft.EntityFrameworkCore;  // Importa o namespace para funcionalidades do Entity Framework Core
using System;  // Importa o namespace para funcionalidades básicas do sistema
using System.Linq;  // Importa o namespace para funcionalidades de consultas LINQ
using System.Threading.Tasks;  // Importa o namespace para funcionalidades assíncronas
using Microsoft.AspNetCore.Authorization;  // Importa o namespace para funcionalidades de autorização

namespace MyPokedexAPI.Controllers  // Define o namespace para o controlador da API
{
    [Authorize]  // Indica que este controlador requer autorização
    [ApiController]  // Indica que esta classe é um controlador de API
    [Route("api/[controller]")]  // Define a rota para aceder a este controlador
    public class PacksController : ControllerBase  // Define a classe PacksController que herda de ControllerBase
    {
        private readonly ApplicationDbContext _context;  // Campo para o contexto da base de dados

        public PacksController(ApplicationDbContext context)  // Construtor que inicializa o campo _context
        {
            _context = context;
        }

        [HttpGet("GetMostPopularPacks")]  // Define um endpoint HTTP GET na rota "GetMostPopularPacks"
        public async Task<IActionResult> GetMostPopularPacks()  // Método para obter os pacotes mais populares
        {
            var mostPopularPacks = await _context.Packs  // Consulta para obter os pacotes mais populares
                .OrderByDescending(p => p.TotalBought)  // Ordena os pacotes pelo total comprado, em ordem decrescente
                .Take(5)  // Toma os 5 primeiros resultados
                .Select(p => new PackDTO  // Projeta os resultados para o DTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    BronzeChance = p.BronzeChance,
                    SilverChance = p.SilverChance,
                    GoldChance = p.GoldChance,
                    PlatinumChance = p.PlatinumChance,
                    DiamondChance = p.DiamondChance,
                    TotalBought = p.TotalBought,
                    CreatedOn = p.CreatedOn,
                    CreatedBy = p.CreatedBy,
                    UpdatedOn = p.UpdatedOn,
                    UpdatedBy = p.UpdatedBy,
                    Image = p.Image != null ? Convert.ToBase64String(p.Image) : null  // Converte a imagem para base64, se existir
                })
                .ToListAsync();  // Converte o resultado para uma lista de forma assíncrona

            return Ok(mostPopularPacks);  // Retorna os pacotes mais populares
        }

        [HttpGet("GetPacksWithPaginationAndSearch")]  // Define um endpoint HTTP GET na rota "GetPacksWithPaginationAndSearch"
        public async Task<IActionResult> GetPacksWithPaginationAndSearch(int page, int maxRecords, string? searchKeyword = "")  // Método para obter pacotes com paginação e pesquisa
        {
            var query = _context.Packs.AsQueryable();  // Cria uma consulta inicial para os pacotes

            if (!string.IsNullOrEmpty(searchKeyword))  // Se a palavra-chave de pesquisa não estiver vazia
            {
                searchKeyword = searchKeyword.Trim().ToLower();  // Remove espaços em branco e converte para minúsculas
                query = query.Where(p => p.Name.ToLower().Contains(searchKeyword));  // Filtra os pacotes pelo nome
            }

            query = query.OrderByDescending(p => p.TotalBought);  // Ordena os pacotes pelo total comprado, em ordem decrescente

            var totalPacks = await query.CountAsync();  // Conta o total de pacotes
            var packs = await query  // Consulta para obter os pacotes com paginação
                .Skip((page - 1) * maxRecords)  // Pula os pacotes das páginas anteriores
                .Take(maxRecords)  // Toma o número máximo de pacotes para a página atual
                .Select(p => new PackDTO  // Projeta os resultados para o DTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    BronzeChance = p.BronzeChance,
                    SilverChance = p.SilverChance,
                    GoldChance = p.GoldChance,
                    PlatinumChance = p.PlatinumChance,
                    DiamondChance = p.DiamondChance,
                    TotalBought = p.TotalBought,
                    CreatedOn = p.CreatedOn,
                    CreatedBy = p.CreatedBy,
                    UpdatedOn = p.UpdatedOn,
                    UpdatedBy = p.UpdatedBy,
                    Image = p.Image != null ? Convert.ToBase64String(p.Image) : null  // Converte a imagem para base64, se existir
                })
                .ToListAsync();  // Converte o resultado para uma lista de forma assíncrona

            return Ok(new { totalPacks, packs });  // Retorna o total de pacotes e os pacotes da página atual
        }

        [HttpPost("CreateOrUpdatePack")]  // Define um endpoint HTTP POST na rota "CreateOrUpdatePack"
        public async Task<IActionResult> CreateOrUpdatePack([FromBody] PackDTO packDto)  // Método para criar ou atualizar um pacote
        {
            if (packDto == null)  // Verifica se o DTO do pacote é nulo
            {
                return BadRequest();  // Retorna um erro de pedido inválido
            }

            Pack pack;
            if (packDto.Id == 0)  // Se o ID do pacote for zero, cria um novo pacote
            {
                pack = new Pack
                {
                    Name = packDto.Name,
                    Price = packDto.Price,
                    Image = !string.IsNullOrEmpty(packDto.Image) ? Convert.FromBase64String(packDto.Image) : null,  // Converte a imagem de base64, se existir
                    BronzeChance = packDto.BronzeChance,
                    SilverChance = packDto.SilverChance,
                    GoldChance = packDto.GoldChance,
                    PlatinumChance = packDto.PlatinumChance,
                    DiamondChance = packDto.DiamondChance,
                    TotalBought = packDto.TotalBought,
                    CreatedOn = DateTime.SpecifyKind(packDto.CreatedOn, DateTimeKind.Utc),
                    CreatedBy = packDto.CreatedBy,
                    UpdatedOn = packDto.UpdatedOn.HasValue ? DateTime.SpecifyKind(packDto.UpdatedOn.Value, DateTimeKind.Utc) : (DateTime?)null,
                    UpdatedBy = packDto.UpdatedBy
                };

                await _context.Packs.AddAsync(pack);  // Adiciona o novo pacote ao contexto
                await _context.SaveChangesAsync();  // Salva as alterações na base de dados

                packDto.Id = pack.Id;  // Atualiza o ID no DTO
            }
            else  // Se o ID do pacote não for zero, atualiza um pacote existente
            {
                pack = await _context.Packs.FindAsync(packDto.Id);  // Procura o pacote na base de dados
                if (pack == null)  // Se o pacote não for encontrado
                {
                    return NotFound();  // Retorna um erro de não encontrado
                }

                pack.Name = packDto.Name;
                pack.Price = packDto.Price;
                pack.Image = !string.IsNullOrEmpty(packDto.Image) ? Convert.FromBase64String(packDto.Image) : null;  // Converte a imagem de base64, se existir
                pack.BronzeChance = packDto.BronzeChance;
                pack.SilverChance = packDto.SilverChance;
                pack.GoldChance = packDto.GoldChance;
                pack.PlatinumChance = packDto.PlatinumChance;
                pack.DiamondChance = packDto.DiamondChance;
                pack.TotalBought = packDto.TotalBought;
                pack.CreatedOn = DateTime.SpecifyKind(packDto.CreatedOn, DateTimeKind.Utc);
                pack.CreatedBy = packDto.CreatedBy;
                pack.UpdatedOn = packDto.UpdatedOn.HasValue ? DateTime.SpecifyKind(packDto.UpdatedOn.Value, DateTimeKind.Utc) : (DateTime?)null;
                pack.UpdatedBy = packDto.UpdatedBy;

                _context.Packs.Update(pack);  // Atualiza o pacote no contexto
                await _context.SaveChangesAsync();  // Salva as alterações na base de dados

                packDto.Id = pack.Id;  // Atualiza o ID no DTO
            }

            // Retorna o DTO atualizado com o ID do pack criado ou atualizado
            return Ok(packDto);
        }

        [HttpDelete("DeletePack/{id}")]  // Define um endpoint HTTP DELETE na rota "DeletePack/{id}"
        public async Task<IActionResult> DeletePack(int id)  // Método para deletar um pacote
        {
            var pack = await _context.Packs.FindAsync(id);  // Procura o pacote na base de dados
            if (pack == null)  // Se o pacote não for encontrado
            {
                return NotFound();  // Retorna um erro de não encontrado
            }

            _context.Packs.Remove(pack);  // Remove o pacote do contexto
            await _context.SaveChangesAsync();  // Salva as alterações na base de dados

            return Ok();  // Retorna uma resposta de sucesso
        }
    }
}
