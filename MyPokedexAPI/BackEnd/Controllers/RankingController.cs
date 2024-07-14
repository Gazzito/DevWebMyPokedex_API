using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPokedexAPI.Data;
using MyPokedexAPI.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MyPokedexAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RankingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RankingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetTopTenPlayersWithMostOpenedPacks")]
        public async Task<IActionResult> GetTopTenPlayersWithMostOpenedPacks()
        {
            var topPlayers = await _context.TotalPacksOpenedRankings
                .OrderByDescending(r => r.TotalPacksOpened)
                .Take(10)
                .Join(_context.Users,
                      ranking => ranking.Id, // Assumindo que Id em TotalPacksOpenedRankings é o UserId
                      user => user.Id,
                      (ranking, user) => new
                      {
                          UserId = user.Id,
                          UserName = user.Name,
                          TotalPacksOpened = ranking.TotalPacksOpened
                      })
                .ToListAsync();

            return Ok(topPlayers);
        }

        [HttpGet("GetTopTenPlayersWithMostDiamondPokemons")]
        public async Task<IActionResult> GetTopTenPlayersWithMostDiamondPokemons()
        {
            var topPlayers = await _context.TotalDiamondPokemonsRankings
                .OrderByDescending(r => r.TotalDiamondPokemons)
                .Take(10)
                .Join(_context.Users,
                      ranking => ranking.Id, // Assumindo que Id em TotalDiamondPokemonsRankings é o UserId
                      user => user.Id,
                      (ranking, user) => new
                      {
                          UserId = user.Id,
                          UserName = user.Name,
                          TotalDiamondPokemons = ranking.TotalDiamondPokemons
                      })
                .ToListAsync();

            return Ok(topPlayers);
        }

        [HttpGet("GetTopTenPlayersWithMostMoney")]
        public async Task<IActionResult> GetTopTenPlayersWithMostMoney()
        {
            var topPlayers = await _context.UserProfiles
                .OrderByDescending(u => u.Money)
                .Take(10)
                .Join(_context.Users,
                      profile => profile.Id, // Assumindo que Id em UserProfiles é o UserId
                      user => user.Id,
                      (profile, user) => new
                      {
                          UserId = user.Id,
                          UserName = user.Name,
                          Money = profile.Money
                      })
                .ToListAsync();

            return Ok(topPlayers);
        }
    }
}
