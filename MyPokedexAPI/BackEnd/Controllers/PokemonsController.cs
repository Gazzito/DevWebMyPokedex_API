using Microsoft.AspNetCore.Mvc;
using MyPokedexAPI.Data;
using MyPokedexAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MyPokedexAPI.Controllers
{
    [Authorize]
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
public async Task<IActionResult> GetRandomPokemonInPack(int packId, int userId, bool isPackFree)
{
    using var transaction = await _context.Database.BeginTransactionAsync();
    try
    {
        // Fetch the pack details
        var pack = await _context.Packs.FindAsync(packId);
        if (pack == null)
        {
            return NotFound("Pack not found.");
        }

        // Fetch the user details
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        if (isPackFree)
        {
            // Check the last time the user can open a free pack
            if (user.NextOpenExpected.HasValue && DateTime.UtcNow < user.NextOpenExpected.Value)
            {
                return BadRequest(new { Message = $"You can open the next free pack after {user.NextOpenExpected.Value}.", NextOpenExpected = user.NextOpenExpected });
            }
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

        // Ensure there's a TotalPacksOpenedRanking record for the user
        var totalPacksOpenedRanking = await _context.TotalPacksOpenedRankings
            .SingleOrDefaultAsync(t => t.Id == userId);
        if (totalPacksOpenedRanking == null)
        {
            totalPacksOpenedRanking = new TotalPacksOpenedRanking
            {
                Id = userId,
                TotalPacksOpened = 0,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            };
            await _context.TotalPacksOpenedRankings.AddAsync(totalPacksOpenedRanking);
        }
        else
        {
            totalPacksOpenedRanking.TotalPacksOpened += 1;
            _context.TotalPacksOpenedRankings.Update(totalPacksOpenedRanking);
        }

        // Ensure there's a TotalDiamondPokemonsRanking record for the user
        var totalDiamondPokemonsRanking = await _context.TotalDiamondPokemonsRankings
            .SingleOrDefaultAsync(t => t.Id == userId);
        if (totalDiamondPokemonsRanking == null)
        {
            totalDiamondPokemonsRanking = new TotalDiamondPokemonsRanking
            {
                Id = userId,
                TotalDiamondPokemons = 0,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            };
            await _context.TotalDiamondPokemonsRankings.AddAsync(totalDiamondPokemonsRanking);
        }
        else if (rarity == Rarity.Diamond)
        {
            totalDiamondPokemonsRanking.TotalDiamondPokemons += 1;
            _context.TotalDiamondPokemonsRankings.Update(totalDiamondPokemonsRanking);
        }

        // Save to PackUsers for history
        var packUser = new PackUsers
        {
            UserId = userId,
            PackId = packId,
            OpenedOn = DateTime.UtcNow
        };

        await _context.PackUsers.AddAsync(packUser);

        // Update user's NextOpenExpected if it's a free pack
        if (isPackFree)
        {
            user.NextOpenExpected = DateTime.UtcNow.AddMinutes(3); // Example: next open expected in 3 minutes
            _context.Users.Update(user);
        }

        // Save changes to the database
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        // Return result
        if (isPackFree)
        {
            return Ok(new { Pokemon = selectedPokemon.Name, Rarity = rarity.ToString(), NextOpenExpected = user.NextOpenExpected });
        }
        else
        {
            return Ok(new { Pokemon = selectedPokemon.Name, Rarity = rarity.ToString() });
        }
    }
    catch (DbUpdateConcurrencyException ex)
    {
        await transaction.RollbackAsync();
        return Conflict(new { Message = "A concurrency error occurred. Please try again.", Details = ex.Message });
    }
    catch (Exception ex)
    {
        await transaction.RollbackAsync();
        return StatusCode(500, $"Internal server error: {ex.Message}");
    }
}








        [HttpGet("GetAllOwnedPokemonsWithFiltersWithPaginationAndSearch")]
        public async Task<IActionResult> GetAllOwnedPokemonsWithFiltersWithPaginationAndSearch(
    int userId, int page, int maxRecords, bool isLatest,
    string? rarityChoosen, // Tornando opcional
    bool isMostAttackSelected, bool isMostHPSelected,
    bool isMostDefSelected, bool isMostSpeedSelected,
    string searchKeyword = "")
        {
            var query = _context.UserPokemons
                .Where(up => up.UserId == userId)
                .Join(_context.Pokemons,
                      up => up.PokemonId,
                      p => p.Id,
                      (up, p) => new { up, p })
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchKeyword))
            {
                searchKeyword = searchKeyword.Trim().ToLower();
                query = query.Where(up => up.p.Name.ToLower().Contains(searchKeyword));
            }

            if (!string.IsNullOrEmpty(rarityChoosen))
            {
                rarityChoosen = rarityChoosen.ToLower();
                query = query.Where(up => up.up.Rarity.ToLower() == rarityChoosen);
            }

            if (isMostAttackSelected)
            {
                query = query.OrderByDescending(up => up.up.ActualAttackPoints);
            }
            else if (isMostHPSelected)
            {
                query = query.OrderByDescending(up => up.up.ActualHealthPoints);
            }
            else if (isMostDefSelected)
            {
                query = query.OrderByDescending(up => up.up.ActualDefensePoints);
            }
            else if (isMostSpeedSelected)
            {
                query = query.OrderByDescending(up => up.up.ActualSpeedPoints);
            }
            else if (isLatest)
            {
                query = query.OrderByDescending(up => up.up.CreatedOn);
            }
            else
            {
                query = query.OrderBy(up => up.up.CreatedOn);
            }

            var totalPokemons = await query.CountAsync();

            var pokemons = await query
                .Skip((page - 1) * maxRecords)
                .Take(maxRecords)
                .Select(up => new UserPokemonDTO
                {
                    Id = up.up.Id,
                    UserId = up.up.UserId,
                    PokemonId = up.up.PokemonId,
                    PokemonName = up.p.Name,
                    ActualAttackPoints = up.up.ActualAttackPoints,
                    ActualHealthPoints = up.up.ActualHealthPoints,
                    ActualDefensePoints = up.up.ActualDefensePoints,
                    ActualSpeedPoints = up.up.ActualSpeedPoints,
                    TotalCombatPoints = up.up.TotalCombatPoints,
                    Rarity = up.up.Rarity,
                    PackId = up.up.PackId,
                    IsFavourite = up.up.IsFavourite,
                    CreatedOn = up.up.CreatedOn,
                    CreatedBy = up.up.CreatedBy,
                    UpdatedOn = up.up.UpdatedOn,
                    UpdatedBy = up.up.UpdatedBy
                })
                .ToListAsync();

            return Ok(new { totalPokemons, pokemons });
        }
    }
}
