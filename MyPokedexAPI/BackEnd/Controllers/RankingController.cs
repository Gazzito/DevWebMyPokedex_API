using Microsoft.AspNetCore.Mvc;  // Importa o namespace para funcionalidades do MVC no ASP.NET Core
using Microsoft.EntityFrameworkCore;  // Importa o namespace para funcionalidades do Entity Framework Core
using MyPokedexAPI.Data;  // Importa o namespace para acesso ao contexto da base de dados
using MyPokedexAPI.Models;  // Importa o namespace para os modelos da aplicação
using System.Linq;  // Importa o namespace para funcionalidades de consultas LINQ
using System.Threading.Tasks;  // Importa o namespace para funcionalidades assíncronas
using Microsoft.AspNetCore.Authorization;  // Importa o namespace para funcionalidades de autorização

namespace MyPokedexAPI.Controllers  // Define o namespace para o controlador da API
{
    [Authorize]  // Indica que este controlador requer autorização
    [ApiController]  // Indica que esta classe é um controlador de API
    [Route("api/[controller]")]  // Define a rota para aceder a este controlador
    public class RankingController : ControllerBase  // Define a classe RankingController que herda de ControllerBase
    {
        private readonly ApplicationDbContext _context;  // Campo para o contexto da base de dados

        public RankingController(ApplicationDbContext context)  // Construtor que inicializa o campo _context
        {
            _context = context;
        }

        [HttpGet("GetTopTenPlayersWithMostOpenedPacks")]  // Define um endpoint HTTP GET na rota "GetTopTenPlayersWithMostOpenedPacks"
        public async Task<IActionResult> GetTopTenPlayersWithMostOpenedPacks()  // Método para obter os dez melhores jogadores com mais pacotes abertos
        {
            var topPlayers = await _context.TotalPacksOpenedRankings  // Consulta para obter os rankings de pacotes abertos
                .OrderByDescending(r => r.TotalPacksOpened)  // Ordena os rankings em ordem decrescente pelo total de pacotes abertos
                .Take(10)  // Toma os dez primeiros resultados
                .Join(_context.Users,  // Junta os rankings com os utilizadores
                      ranking => ranking.Id,  // Assumindo que Id em TotalPacksOpenedRankings é o UserId
                      user => user.Id,
                      (ranking, user) => new  // Projeta os resultados para um objeto anônimo
                      {
                          UserId = user.Id,
                          UserName = user.Name,
                          TotalPacksOpened = ranking.TotalPacksOpened
                      })
                .ToListAsync();  // Converte o resultado para uma lista de forma assíncrona

            return Ok(topPlayers);  // Retorna os melhores jogadores com mais pacotes abertos
        }

        [HttpGet("GetTopTenPlayersWithMostDiamondPokemons")]  // Define um endpoint HTTP GET na rota "GetTopTenPlayersWithMostDiamondPokemons"
        public async Task<IActionResult> GetTopTenPlayersWithMostDiamondPokemons()  // Método para obter os dez melhores jogadores com mais Pokémons de diamante
        {
            var topPlayers = await _context.TotalDiamondPokemonsRankings  // Consulta para obter os rankings de Pokémons de diamante
                .OrderByDescending(r => r.TotalDiamondPokemons)  // Ordena os rankings em ordem decrescente pelo total de Pokémons de diamante
                .Take(10)  // Toma os dez primeiros resultados
                .Join(_context.Users,  // Junta os rankings com os utilizadores
                      ranking => ranking.Id,  // Assumindo que Id em TotalDiamondPokemonsRankings é o UserId
                      user => user.Id,
                      (ranking, user) => new  // Projeta os resultados para um objeto anônimo
                      {
                          UserId = user.Id,
                          UserName = user.Name,
                          TotalDiamondPokemons = ranking.TotalDiamondPokemons
                      })
                .ToListAsync();  // Converte o resultado para uma lista de forma assíncrona

            return Ok(topPlayers);  // Retorna os melhores jogadores com mais Pokémons de diamante
        }

        [HttpGet("GetTopTenPlayersWithMostMoney")]  // Define um endpoint HTTP GET na rota "GetTopTenPlayersWithMostMoney"
        public async Task<IActionResult> GetTopTenPlayersWithMostMoney()  // Método para obter os dez melhores jogadores com mais dinheiro
        {
            var topPlayers = await _context.UserProfiles  // Consulta para obter os perfis de utilizador
                .OrderByDescending(u => u.Money)  // Ordena os perfis em ordem decrescente pelo total de dinheiro
                .Take(10)  // Toma os dez primeiros resultados
                .Join(_context.Users,  // Junta os perfis com os utilizadores
                      profile => profile.Id,  // Assumindo que Id em UserProfiles é o UserId
                      user => user.Id,
                      (profile, user) => new  // Projeta os resultados para um objeto anônimo
                      {
                          UserId = user.Id,
                          UserName = user.Name,
                          Money = profile.Money
                      })
                .ToListAsync();  // Converte o resultado para uma lista de forma assíncrona

            return Ok(topPlayers);  // Retorna os melhores jogadores com mais dinheiro
        }
    }
}
