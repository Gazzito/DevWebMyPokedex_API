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
    public class RankingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RankingController(ApplicationDbContext context)
        {
            _context = context;
        }
  
       //PARA TESTAR TODOS//
        // Endpoint I: GetTopTenPlayersWithMostOpenedPacks
        [HttpGet("GetTopTenPlayersWithMostOpenedPacks")]
        public async Task<IActionResult> GetTopTenPlayersWithMostOpenedPacks()
        {
            var topPlayers = await _context.TotalPacksOpenedRankings
                .OrderByDescending(t => t.TotalPacksOpened)
                .Take(10)
                .Select(t => new 
                {
                    t.UserId,
                    UserName = t.User.Name,
                    t.TotalPacksOpened
                })
                .ToListAsync();

            return Ok(topPlayers);
        }

        // Endpoint J: GetTopTenPlayersWithMostDiamondPokemons
        [HttpGet("GetTopTenPlayersWithMostDiamondPokemons")]
        public async Task<IActionResult> GetTopTenPlayersWithMostDiamondPokemons()
        {
            var topPlayers = await _context.TotalDiamondPokemonsRankings
                .OrderByDescending(t => t.TotalDiamondPokemons)
                .Take(10)
                .Select(t => new 
                {
                    t.UserId,
                    UserName = t.User.Name,
                    t.TotalDiamondPokemons
                })
                .ToListAsync();

            return Ok(topPlayers);
        }

        // Endpoint K: GetTopTenPlayersWithMostMoney
        [HttpGet("GetTopTenPlayersWithMostMoney")]
        public async Task<IActionResult> GetTopTenPlayersWithMostMoney()
        {
            var topPlayers = await _context.UserProfiles
                .OrderByDescending(u => u.Money)
                .Take(10)
                .Select(u => new 
                {
                    u.UserId,
                    UserName = u.User.Name,
                    u.Money
                })
                .ToListAsync();

            return Ok(topPlayers);
        }
    }
}
