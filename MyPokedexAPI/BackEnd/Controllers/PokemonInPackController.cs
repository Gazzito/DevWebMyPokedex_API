using Microsoft.AspNetCore.Mvc;
using MyPokedexAPI.Data;
using MyPokedexAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
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
                CreatedById = pokemonInPackDto.CreatedById,
                UpdatedOn = DateTime.SpecifyKind(pokemonInPackDto.UpdatedOn, DateTimeKind.Utc),
                UpdatedById = pokemonInPackDto.UpdatedById
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
                existingPokemonInPack.CreatedById = pokemonInPack.CreatedById;
                existingPokemonInPack.UpdatedOn = pokemonInPack.UpdatedOn;
                existingPokemonInPack.UpdatedById = pokemonInPack.UpdatedById;

                _context.PokemonInPacks.Update(existingPokemonInPack);
            }

            await _context.SaveChangesAsync();
            return Ok(pokemonInPack);
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
                .Select(pip => new 
                {
                    pip.Pokemon.Id,
                    pip.Pokemon.Name,
                    pip.Pokemon.BaseAttackPoints,
                    pip.Pokemon.BaseDefensePoints,
                    pip.Pokemon.BaseHealthPoints,
                    pip.Pokemon.BaseSpeedPoints,
                    pip.Pokemon.CreatedOn,
                    pip.Pokemon.UpdatedOn
                })
                .ToListAsync();

            return Ok(pokemonsInPack);
        }
    }
}
