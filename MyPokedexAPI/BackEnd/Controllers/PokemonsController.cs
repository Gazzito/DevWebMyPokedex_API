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

            Pokemon pokemon;
            if (pokemonDto.Id == 0)
            {
                pokemon = new Pokemon
                {
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

                await _context.Pokemons.AddAsync(pokemon);
                await _context.SaveChangesAsync();

                pokemonDto.Id = pokemon.Id;
            }
            else
            {
                pokemon = await _context.Pokemons.FindAsync(pokemonDto.Id);
                if (pokemon == null)
                {
                    return NotFound();
                }

                pokemon.Name = pokemonDto.Name;
                pokemon.RegionId = pokemonDto.RegionId;
                pokemon.BaseAttackPoints = pokemonDto.BaseAttackPoints;
                pokemon.BaseHealthPoints = pokemonDto.BaseHealthPoints;
                pokemon.BaseDefensePoints = pokemonDto.BaseDefensePoints;
                pokemon.BaseSpeedPoints = pokemonDto.BaseSpeedPoints;
                pokemon.CreatedOn = DateTime.SpecifyKind(pokemonDto.CreatedOn, DateTimeKind.Utc);
                pokemon.CreatedBy = pokemonDto.CreatedBy;
                pokemon.UpdatedOn = pokemonDto.UpdatedOn.HasValue ? DateTime.SpecifyKind(pokemonDto.UpdatedOn.Value, DateTimeKind.Utc) : (DateTime?)null;
                pokemon.UpdatedBy = pokemonDto.UpdatedBy;
                pokemon.Image = !string.IsNullOrEmpty(pokemonDto.Image) ? Convert.FromBase64String(pokemonDto.Image) : null;

                _context.Pokemons.Update(pokemon);
                await _context.SaveChangesAsync();

                pokemonDto.Id = pokemon.Id;
            }

            // Retorna o DTO atualizado com o ID do Pokémon criado ou atualizado
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

         [HttpGet("GetRandomPokemonInPack")]
        public async Task<IActionResult> GetRandomPokemonInPack(int packId, int userId)
        {
            // Fetch the pack details
            var pack = await _context.Packs.FindAsync(packId);
            if (pack == null)
            {
                return NotFound("Pack not found.");
            }

            // Fetch the Pokémons in the pack
            var pokemonsInPack = await _context.PokemonInPacks
                .Where(p => p.PackId == packId)
                .Include(p => p.Pokemon)
                .ToListAsync();

            if (pokemonsInPack.Count == 0)
            {
                return NotFound("No Pokémons found in this pack.");
            }

            // Randomize a Pokémon
            var random = new Random();
            var selectedPokemonInPack = pokemonsInPack[random.Next(pokemonsInPack.Count)];
            var selectedPokemon = selectedPokemonInPack.Pokemon;

            // Randomize rarity
            double roll = random.NextDouble() * 100;
            Rarity rarity;
            if (roll < pack.BronzeChance) rarity = Rarity.Bronze;
            else if (roll < pack.BronzeChance + pack.SilverChance) rarity = Rarity.Silver;
            else if (roll < pack.BronzeChance + pack.SilverChance + pack.GoldChance) rarity = Rarity.Gold;
            else if (roll < pack.BronzeChance + pack.SilverChance + pack.GoldChance + pack.PlatinumChance) rarity = Rarity.Platinum;
            else rarity = Rarity.Diamond;

            // Adjust stats based on rarity
            double multiplier = rarity switch
            {
                Rarity.Bronze => 2.5,
                Rarity.Silver => 3.75,
                Rarity.Gold => 4,
                Rarity.Platinum => 6,
                Rarity.Diamond => 10,
                _ => 1,
            };

            var userPokemon = new UserPokemons
            {
                UserId = userId,
                PokemonId = selectedPokemon.Id,
                ActualAttackPoints = (int)(selectedPokemon.BaseAttackPoints * multiplier),
                ActualHealthPoints = (int)(selectedPokemon.BaseHealthPoints * multiplier),
                ActualDefensePoints = (int)(selectedPokemon.BaseDefensePoints * multiplier),
                ActualSpeedPoints = (int)(selectedPokemon.BaseSpeedPoints * multiplier),
                TotalCombatPoints = (int)(selectedPokemon.BaseAttackPoints * multiplier +
                                           selectedPokemon.BaseHealthPoints * multiplier +
                                           selectedPokemon.BaseDefensePoints * multiplier +
                                           selectedPokemon.BaseSpeedPoints * multiplier),
                Rarity = rarity.ToString(),
                PackId = packId,
                IsFavourite = false,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = userId,
                UpdatedOn = null,
                UpdatedBy = null
            };

            await _context.UserPokemons.AddAsync(userPokemon);

            // Update TotalPacksOpenedRanking
            var totalPacksOpenedRanking = await _context.TotalPacksOpenedRankings
                .FirstOrDefaultAsync(t => t.Id == userId);
            if (totalPacksOpenedRanking != null)
            {
                totalPacksOpenedRanking.TotalPacksOpened += 1;
                _context.TotalPacksOpenedRankings.Update(totalPacksOpenedRanking);
            }

            // Update TotalDiamondPokemonsRanking if necessary
            if (rarity == Rarity.Diamond)
            {
                var totalDiamondPokemonsRanking = await _context.TotalDiamondPokemonsRankings
                    .FirstOrDefaultAsync(t => t.Id == userId);
                if (totalDiamondPokemonsRanking != null)
                {
                    totalDiamondPokemonsRanking.TotalDiamondPokemons += 1;
                    _context.TotalDiamondPokemonsRankings.Update(totalDiamondPokemonsRanking);
                }
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(new { Pokemon = selectedPokemon.Name, Rarity = rarity.ToString() });
        }
    }
}
