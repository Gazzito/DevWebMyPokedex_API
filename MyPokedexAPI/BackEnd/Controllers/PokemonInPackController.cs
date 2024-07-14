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
    public class PokemonInPackController : ControllerBase  // Define a classe PokemonInPackController que herda de ControllerBase
    {
        private readonly ApplicationDbContext _context;  // Campo para o contexto da base de dados

        public PokemonInPackController(ApplicationDbContext context)  // Construtor que inicializa o campo _context
        {
            _context = context;
        }

        [HttpGet("GetPokemonsInPack")]  // Define um endpoint HTTP GET na rota "GetPokemonsInPack"
        public async Task<IActionResult> GetPokemonsInPack(int packId)  // Método para obter os Pokémons em um pack específico
        {
            var pokemonsInPack = await _context.PokemonInPacks  // Consulta para obter os Pokémons no pack especificado
                .Where(pip => pip.PackId == packId)  // Filtra os Pokémons pelo ID do pack
                .Include(pip => pip.Pokemon)  // Inclui os dados do Pokémon relacionado
                .Select(pip => new PokemonDTO  // Projeta os resultados para o DTO
                {
                    Id = pip.Pokemon.Id,
                    Name = pip.Pokemon.Name,
                    RegionId = pip.Pokemon.RegionId,
                    BaseAttackPoints = pip.Pokemon.BaseAttackPoints,
                    BaseHealthPoints = pip.Pokemon.BaseHealthPoints,
                    BaseDefensePoints = pip.Pokemon.BaseDefensePoints,
                    BaseSpeedPoints = pip.Pokemon.BaseSpeedPoints,
                    CreatedOn = pip.Pokemon.CreatedOn,
                    CreatedBy = pip.Pokemon.CreatedBy,
                    UpdatedOn = pip.Pokemon.UpdatedOn,
                    UpdatedBy = pip.Pokemon.UpdatedBy,
                    Image = pip.Pokemon.Image != null ? Convert.ToBase64String(pip.Pokemon.Image) : null  // Converte a imagem para base64, se existir
                })
                .ToListAsync();  // Converte o resultado para uma lista de forma assíncrona

            return Ok(pokemonsInPack);  // Retorna os Pokémons no pack
        }

        [HttpPost("CreateOrUpdatePokemonInPack")]  // Define um endpoint HTTP POST na rota "CreateOrUpdatePokemonInPack"
        public async Task<IActionResult> CreateOrUpdatePokemonInPack([FromBody] PokemonInPackDTO pokemonInPackDto)  // Método para criar ou atualizar um Pokémon em um pack
        {
            if (pokemonInPackDto == null)  // Verifica se o DTO do Pokémon em pack é nulo
            {
                return BadRequest();  // Retorna um erro de pedido inválido
            }

            PokemonInPack pokemonInPack;
            if (pokemonInPackDto.Id == 0)  // Se o ID do Pokémon em pack for zero, cria um novo registro
            {
                pokemonInPack = new PokemonInPack
                {
                    PackId = pokemonInPackDto.PackId,
                    PokemonId = pokemonInPackDto.PokemonId,
                    CreatedOn = DateTime.SpecifyKind(pokemonInPackDto.CreatedOn, DateTimeKind.Utc),
                    CreatedBy = pokemonInPackDto.CreatedBy,
                    UpdatedOn = pokemonInPackDto.UpdatedOn.HasValue ? DateTime.SpecifyKind(pokemonInPackDto.UpdatedOn.Value, DateTimeKind.Utc) : (DateTime?)null,
                    UpdatedBy = pokemonInPackDto.UpdatedBy
                };

                await _context.PokemonInPacks.AddAsync(pokemonInPack);  // Adiciona o novo Pokémon em pack ao contexto
                await _context.SaveChangesAsync();  // Salva as alterações na base de dados

                pokemonInPackDto.Id = pokemonInPack.Id;  // Atualiza o ID no DTO
            }
            else  // Se o ID do Pokémon em pack não for zero, atualiza um registro existente
            {
                pokemonInPack = await _context.PokemonInPacks.FindAsync(pokemonInPackDto.Id);  // Procura o Pokémon em pack na base de dados
                if (pokemonInPack == null)  // Se o Pokémon em pack não for encontrado
                {
                    return NotFound();  // Retorna um erro de não encontrado
                }

                pokemonInPack.PackId = pokemonInPackDto.PackId;
                pokemonInPack.PokemonId = pokemonInPackDto.PokemonId;
                pokemonInPack.CreatedOn = DateTime.SpecifyKind(pokemonInPackDto.CreatedOn, DateTimeKind.Utc);
                pokemonInPack.CreatedBy = pokemonInPackDto.CreatedBy;
                pokemonInPack.UpdatedOn = pokemonInPackDto.UpdatedOn.HasValue ? DateTime.SpecifyKind(pokemonInPackDto.UpdatedOn.Value, DateTimeKind.Utc) : (DateTime?)null;
                pokemonInPack.UpdatedBy = pokemonInPackDto.UpdatedBy;

                _context.PokemonInPacks.Update(pokemonInPack);  // Atualiza o Pokémon em pack no contexto
                await _context.SaveChangesAsync();  // Salva as alterações na base de dados
            }

            // Retorna o DTO atualizado com o ID do PokemonInPack criado ou atualizado
            return Ok(pokemonInPackDto);
        }

        [HttpDelete("DeletePokemonInPack/{id}")]  // Define um endpoint HTTP DELETE na rota "DeletePokemonInPack/{id}"
        public async Task<IActionResult> DeletePokemonInPack(int id)  // Método para deletar um Pokémon de um pack
        {
            var pokemonInPack = await _context.PokemonInPacks.FindAsync(id);  // Procura o Pokémon em pack na base de dados
            if (pokemonInPack == null)  // Se o Pokémon em pack não for encontrado
            {
                return NotFound();  // Retorna um erro de não encontrado
            }

            _context.PokemonInPacks.Remove(pokemonInPack);  // Remove o Pokémon em pack do contexto
            await _context.SaveChangesAsync();  // Salva as alterações na base de dados

            return Ok();  // Retorna uma resposta de sucesso
        }

        [HttpGet("GetPackPokemonsbyPackId/{packId}")]  // Define um endpoint HTTP GET na rota "GetPackPokemonsbyPackId/{packId}"
        public async Task<IActionResult> GetPackPokemonsbyPackId(int packId)  // Método para obter os Pokémons em um pack específico pelo ID do pack
        {
            var pokemonsInPack = await _context.PokemonInPacks  // Consulta para obter os Pokémons no pack especificado
                .Where(pip => pip.PackId == packId)  // Filtra os Pokémons pelo ID do pack
                .Include(pip => pip.Pokemon)  // Inclui os dados do Pokémon relacionado
                .Select(pip => new PokemonDTO  // Projeta os resultados para o DTO
                {
                    Id = pip.Pokemon.Id,
                    Name = pip.Pokemon.Name,
                    BaseAttackPoints = pip.Pokemon.BaseAttackPoints,
                    BaseDefensePoints = pip.Pokemon.BaseDefensePoints,
                    BaseHealthPoints = pip.Pokemon.BaseHealthPoints,
                    BaseSpeedPoints = pip.Pokemon.BaseSpeedPoints,
                    CreatedOn = pip.Pokemon.CreatedOn,
                    RegionId = pip.Pokemon.RegionId,
                    Image = pip.Pokemon.Image != null ? Convert.ToBase64String(pip.Pokemon.Image) : null,
                    UpdatedOn = pip.Pokemon.UpdatedOn,
                    CreatedBy = pip.Pokemon.CreatedBy
                })
                .ToListAsync();  // Converte o resultado para uma lista de forma assíncrona

            return Ok(pokemonsInPack);  // Retorna os Pokémons no pack
        }
    }
}
