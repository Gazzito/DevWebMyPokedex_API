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

        [HttpPost("CreateOrUpdatePokemonInPack")]
        public async Task<IActionResult> CreateOrUpdatePokemonInPack([FromBody] PokemonInPackDTO pokemonInPackDto)
        {
            if (pokemonInPackDto == null)
            {
                return BadRequest();
            }

            var pokemonInPack = new PokemonInPack
            {
                Id = pokemonInPackDto.Id,
                PackId = pokemonInPackDto.PackId,
                PokemonId = pokemonInPackDto.PokemonId,
                CreatedOn = DateTime.SpecifyKind(pokemonInPackDto.CreatedOn, DateTimeKind.Utc),
                CreatedBy = pokemonInPackDto.CreatedBy,
                UpdatedOn = pokemonInPackDto.UpdatedOn.HasValue ? DateTime.SpecifyKind(pokemonInPackDto.UpdatedOn.Value, DateTimeKind.Utc) : (DateTime?)null,
                UpdatedBy = pokemonInPackDto.UpdatedBy
            };

            if (pokemonInPack.Id == 0)
            {
                await _context.PokemonInPacks.AddAsync(pokemonInPack);
            }
            else
            {
                var existingPokemonInPack = await _context.PokemonInPacks.FindAsync(pokemonInPack.Id);
                if (existingPokemonInPack == null)
                {
                    return NotFound();
                }

                existingPokemonInPack.PackId = pokemonInPack.PackId;
                existingPokemonInPack.PokemonId = pokemonInPack.PokemonId;
                existingPokemonInPack.CreatedOn = pokemonInPack.CreatedOn;
                existingPokemonInPack.CreatedBy = pokemonInPack.CreatedBy;
                existingPokemonInPack.UpdatedOn = pokemonInPack.UpdatedOn;
                existingPokemonInPack.UpdatedBy = pokemonInPack.UpdatedBy;

                _context.PokemonInPacks.Update(existingPokemonInPack);
            }

            await _context.SaveChangesAsync();
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
