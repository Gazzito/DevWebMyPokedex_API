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
    public class PokemonsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PokemonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllPaginatedPokemonsWithPaginationAndSearch")]
        public async Task<IActionResult> GetAllPaginatedPokemonsWithPaginationAndSearch(int page, int maxRecords, string searchKeyword)
        {
            var query = _context.Pokemons.AsQueryable();

            if (!string.IsNullOrEmpty(searchKeyword))
            {
                searchKeyword = searchKeyword.Trim().ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(searchKeyword));
            }

            var totalPokemons = await query.CountAsync();
            var pokemons = await query
                .Skip((page - 1) * maxRecords)
                .Take(maxRecords)
                .Select(p => new PokemonDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    RegionId = p.RegionId,
                    BaseAttackPoints = p.BaseAttackPoints,
                    BaseHealthPoints = p.BaseHealthPoints,
                    BaseDefensePoints = p.BaseDefensePoints,
                    BaseSpeedPoints = p.BaseSpeedPoints,
                    CreatedOn = p.CreatedOn,
                    CreatedBy = p.CreatedBy,
                    UpdatedOn = p.UpdatedOn,
                    UpdatedBy = p.UpdatedBy,
                    Image = p.Image != null ? Convert.ToBase64String(p.Image) : null
                })
                .ToListAsync();

            return Ok(new { totalPokemons, pokemons });
        }

        [HttpPost("CreateOrUpdatePokemon")]
        public async Task<IActionResult> CreateOrUpdatePokemon([FromBody] PokemonDTO pokemonDto)
        {
            if (pokemonDto == null)
            {
                return BadRequest();
            }

            var pokemon = new Pokemon
            {
                Id = pokemonDto.Id,
                Name = pokemonDto.Name,
                RegionId = pokemonDto.RegionId,
                BaseAttackPoints = pokemonDto.BaseAttackPoints,
                BaseHealthPoints = pokemonDto.BaseHealthPoints,
                BaseDefensePoints = pokemonDto.BaseDefensePoints,
                BaseSpeedPoints = pokemonDto.BaseSpeedPoints,
                CreatedOn = DateTime.SpecifyKind(pokemonDto.CreatedOn, DateTimeKind.Utc),
                CreatedBy = pokemonDto.CreatedBy,
                UpdatedOn = pokemonDto.UpdatedOn.HasValue ? DateTime.SpecifyKind(pokemonDto.UpdatedOn.Value, DateTimeKind.Utc) : (DateTime?)null,
                UpdatedBy = pokemonDto.UpdatedBy,
                Image = !string.IsNullOrEmpty(pokemonDto.Image) ? Convert.FromBase64String(pokemonDto.Image) : null
            };

            if (pokemon.Id == 0)
            {
                await _context.Pokemons.AddAsync(pokemon);
            }
            else
            {
                var existingPokemon = await _context.Pokemons.FindAsync(pokemon.Id);
                if (existingPokemon == null)
                {
                    return NotFound();
                }

                existingPokemon.Name = pokemon.Name;
                existingPokemon.RegionId = pokemon.RegionId;
                existingPokemon.BaseAttackPoints = pokemon.BaseAttackPoints;
                existingPokemon.BaseHealthPoints = pokemon.BaseHealthPoints;
                existingPokemon.BaseDefensePoints = pokemon.BaseDefensePoints;
                existingPokemon.BaseSpeedPoints = pokemon.BaseSpeedPoints;
                existingPokemon.CreatedOn = pokemon.CreatedOn;
                existingPokemon.CreatedBy = pokemon.CreatedBy;
                existingPokemon.UpdatedOn = pokemon.UpdatedOn;
                existingPokemon.UpdatedBy = pokemon.UpdatedBy;
                existingPokemon.Image = pokemon.Image;

                _context.Pokemons.Update(existingPokemon);
            }

            await _context.SaveChangesAsync();
            return Ok(pokemonDto);
        }

        [HttpDelete("DeletePokemon/{id}")]
        public async Task<IActionResult> DeletePokemon(int id)
        {
            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }

            _context.Pokemons.Remove(pokemon);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("GetMoneyFromPokemon")]
        public async Task<IActionResult> GetMoneyFromPokemon(int userId, int pokemonId, Rarity pokemonRarity)
        {
            // Find the UserPokemon record
            var userPokemon = await _context.UserPokemons
                .FirstOrDefaultAsync(up => up.UserId == userId && up.PokemonId == pokemonId);

            if (userPokemon == null)
            {
                return NotFound("Pokemon not found for this user.");
            }

            // Calculate the sale price based on rarity
            int salePrice = pokemonRarity switch
            {
                Rarity.Bronze => 250,
                Rarity.Silver => 500,
                Rarity.Gold => 1000,
                Rarity.Platinum => 2500,
                Rarity.Diamond => 5000,
                _ => 0,
            };

            // Remove the UserPokemon record
            _context.UserPokemons.Remove(userPokemon);

            // Update TotalDiamondPokemonsRanking if the pokemon is of Diamond rarity
            if (pokemonRarity == Rarity.Diamond)
            {
                var totalDiamondPokemonsRanking = await _context.TotalDiamondPokemonsRankings
                    .FirstOrDefaultAsync(t => t.Id == userId);

                if (totalDiamondPokemonsRanking != null)
                {
                    totalDiamondPokemonsRanking.TotalDiamondPokemons -= 1;
                    _context.TotalDiamondPokemonsRankings.Update(totalDiamondPokemonsRanking);
                }
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the sale price
            return Ok(new { SalePrice = salePrice });
        }
    }
}
