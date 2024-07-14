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
    public class PokemonInPackController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PokemonInPackController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("GetPokemonsInPack")]
        public async Task<IActionResult> GetPokemonsInPack(int packId)
        {
            var pokemonsInPack = await _context.PokemonInPacks
                .Where(pip => pip.PackId == packId)
                .Include(pip => pip.Pokemon)
                .Select(pip => new PokemonDTO
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
                    Image = pip.Pokemon.Image != null ? Convert.ToBase64String(pip.Pokemon.Image) : null
                })
                .ToListAsync();

            return Ok(pokemonsInPack);
        }


        [HttpPost("CreateOrUpdatePokemonInPack")]
        public async Task<IActionResult> CreateOrUpdatePokemonInPack([FromBody] PokemonInPackDTO pokemonInPackDto)
        {
            if (pokemonInPackDto == null)
            {
                return BadRequest();
            }

            PokemonInPack pokemonInPack;
            if (pokemonInPackDto.Id == 0)
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

                await _context.PokemonInPacks.AddAsync(pokemonInPack);
                await _context.SaveChangesAsync();

                pokemonInPackDto.Id = pokemonInPack.Id;
            }
            else
            {
                pokemonInPack = await _context.PokemonInPacks.FindAsync(pokemonInPackDto.Id);
                if (pokemonInPack == null)
                {
                    return NotFound();
                }

                pokemonInPack.PackId = pokemonInPackDto.PackId;
                pokemonInPack.PokemonId = pokemonInPackDto.PokemonId;
                pokemonInPack.CreatedOn = DateTime.SpecifyKind(pokemonInPackDto.CreatedOn, DateTimeKind.Utc);
                pokemonInPack.CreatedBy = pokemonInPackDto.CreatedBy;
                pokemonInPack.UpdatedOn = pokemonInPackDto.UpdatedOn.HasValue ? DateTime.SpecifyKind(pokemonInPackDto.UpdatedOn.Value, DateTimeKind.Utc) : (DateTime?)null;
                pokemonInPack.UpdatedBy = pokemonInPackDto.UpdatedBy;

                _context.PokemonInPacks.Update(pokemonInPack);
                await _context.SaveChangesAsync();
            }

            // Retorna o DTO atualizado com o ID do PokemonInPack criado ou atualizado
            return Ok(pokemonInPackDto);
        }

        [HttpDelete("DeletePokemonInPack/{id}")]
        public async Task<IActionResult> DeletePokemonInPack(int id)
        {
            var pokemonInPack = await _context.PokemonInPacks.FindAsync(id);
            if (pokemonInPack == null)
            {
                return NotFound();
            }

            _context.PokemonInPacks.Remove(pokemonInPack);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("GetPackPokemonsbyPackId/{packId}")]
        public async Task<IActionResult> GetPackPokemonsbyPackId(int packId)
        {
            var pokemonsInPack = await _context.PokemonInPacks
                .Where(pip => pip.PackId == packId)
                .Include(pip => pip.Pokemon)
                .Select(pip => new PokemonDTO
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
                .ToListAsync();

            return Ok(pokemonsInPack);
        }
    }
}
