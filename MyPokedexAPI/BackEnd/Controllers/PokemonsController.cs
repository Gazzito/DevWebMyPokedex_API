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
    public class PokemonsController : ControllerBase  // Define a classe PokemonsController que herda de ControllerBase
    {
        private readonly ApplicationDbContext _context;  // Campo para o contexto da base de dados

        public PokemonsController(ApplicationDbContext context)  // Construtor que inicializa o campo _context
        {
            _context = context;
        }

        [HttpGet("GetAllPaginatedPokemonsWithPaginationAndSearch")]  // Define um endpoint HTTP GET na rota "GetAllPaginatedPokemonsWithPaginationAndSearch"
        public async Task<IActionResult> GetAllPaginatedPokemonsWithPaginationAndSearch(int page, int maxRecords, string searchKeyword)  // Método para obter todos os Pokémons com paginação e pesquisa
        {
            var query = _context.Pokemons.AsQueryable();  // Cria uma consulta inicial para os Pokémons

            if (!string.IsNullOrEmpty(searchKeyword))  // Se a palavra-chave de pesquisa não estiver vazia
            {
                searchKeyword = searchKeyword.Trim().ToLower();  // Remove espaços em branco e converte para minúsculas
                query = query.Where(p => p.Name.ToLower().Contains(searchKeyword));  // Filtra os Pokémons pelo nome
            }

            var totalPokemons = await query.CountAsync();  // Conta o total de Pokémons
            var pokemons = await query  // Consulta para obter os Pokémons com paginação
                .Skip((page - 1) * maxRecords)  // Pula os Pokémons das páginas anteriores
                .Take(maxRecords)  // Toma o número máximo de Pokémons para a página atual
                .Select(p => new PokemonDTO  // Projeta os resultados para o DTO
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
                    Image = p.Image != null ? Convert.ToBase64String(p.Image) : null  // Converte a imagem para base64, se existir
                })
                .ToListAsync();  // Converte o resultado para uma lista de forma assíncrona

            return Ok(new { totalPokemons, pokemons });  // Retorna o total de Pokémons e os Pokémons da página atual
        }

        [HttpPost("CreateOrUpdatePokemon")]  // Define um endpoint HTTP POST na rota "CreateOrUpdatePokemon"
        public async Task<IActionResult> CreateOrUpdatePokemon([FromBody] PokemonDTO pokemonDto)  // Método para criar ou atualizar um Pokémon
        {
            if (pokemonDto == null)  // Verifica se o DTO do Pokémon é nulo
            {
                return BadRequest();  // Retorna um erro de pedido inválido
            }

            Pokemon pokemon;
            if (pokemonDto.Id == 0)  // Se o ID do Pokémon for zero, cria um novo Pokémon
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
                    Image = !string.IsNullOrEmpty(pokemonDto.Image) ? Convert.FromBase64String(pokemonDto.Image) : null  // Converte a imagem de base64, se existir
                };

                await _context.Pokemons.AddAsync(pokemon);  // Adiciona o novo Pokémon ao contexto
                await _context.SaveChangesAsync();  // Salva as alterações na base de dados

                pokemonDto.Id = pokemon.Id;  // Atualiza o ID no DTO
            }
            else  // Se o ID do Pokémon não for zero, atualiza um Pokémon existente
            {
                pokemon = await _context.Pokemons.FindAsync(pokemonDto.Id);  // Procura o Pokémon na base de dados
                if (pokemon == null)  // Se o Pokémon não for encontrado
                {
                    return NotFound();  // Retorna um erro de não encontrado
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
                pokemon.Image = !string.IsNullOrEmpty(pokemonDto.Image) ? Convert.FromBase64String(pokemonDto.Image) : null;  // Converte a imagem de base64, se existir

                _context.Pokemons.Update(pokemon);  // Atualiza o Pokémon no contexto
                await _context.SaveChangesAsync();  // Salva as alterações na base de dados

                pokemonDto.Id = pokemon.Id;  // Atualiza o ID no DTO
            }

            // Retorna o DTO atualizado com o ID do Pokémon criado ou atualizado
            return Ok(pokemonDto);
        }

        [HttpDelete("DeletePokemon/{id}")]  // Define um endpoint HTTP DELETE na rota "DeletePokemon/{id}"
        public async Task<IActionResult> DeletePokemon(int id)  // Método para deletar um Pokémon
        {
            var pokemon = await _context.Pokemons.FindAsync(id);  // Procura o Pokémon na base de dados
            if (pokemon == null)  // Se o Pokémon não for encontrado
            {
                return NotFound();  // Retorna um erro de não encontrado
            }

            _context.Pokemons.Remove(pokemon);  // Remove o Pokémon do contexto
            await _context.SaveChangesAsync();  // Salva as alterações na base de dados

            return Ok();  // Retorna uma resposta de sucesso
        }

        [HttpGet("GetRandomPokemonInPack")]  // Define um endpoint HTTP GET na rota "GetRandomPokemonInPack"
        public async Task<IActionResult> GetRandomPokemonInPack(int packId, int userId, bool isPackFree)  // Método para obter um Pokémon aleatório de um pack
        {
            using var transaction = await _context.Database.BeginTransactionAsync();  // Inicia uma transação
            try
            {
                // Busca os detalhes do pack
                var pack = await _context.Packs.FindAsync(packId);
                if (pack == null)
                {
                    return NotFound("Pack not found.");  // Retorna um erro se o pack não for encontrado
                }

                // Busca os detalhes do utilizador
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    return NotFound("User not found.");  // Retorna um erro se o utilizador não for encontrado
                }

                if (isPackFree)
                {
                    // Verifica a última vez que o utilizador pode abrir um pack gratuito
                    if (user.NextOpenExpected.HasValue && DateTime.UtcNow < user.NextOpenExpected.Value)
                    {
                        return BadRequest(new { Message = $"You can open the next free pack after {user.NextOpenExpected.Value}.", NextOpenExpected = user.NextOpenExpected });
                    }
                }

                // Busca os Pokémons no pack
                var pokemonsInPack = await _context.PokemonInPacks
                    .Where(p => p.PackId == packId)
                    .Include(p => p.Pokemon)
                    .ToListAsync();

                if (pokemonsInPack.Count == 0)
                {
                    return NotFound("No Pokémons found in this pack.");  // Retorna um erro se não houver Pokémons no pack
                }

                // Aleatoriza um Pokémon
                var random = new Random();
                var selectedPokemonInPack = pokemonsInPack[random.Next(pokemonsInPack.Count)];
                var selectedPokemon = selectedPokemonInPack.Pokemon;

                // Aleatoriza a raridade
                double roll = random.NextDouble() * 100;
                Rarity rarity;
                if (roll < pack.BronzeChance) rarity = Rarity.Bronze;
                else if (roll < pack.BronzeChance + pack.SilverChance) rarity = Rarity.Silver;
                else if (roll < pack.BronzeChance + pack.SilverChance + pack.GoldChance) rarity = Rarity.Gold;
                else if (roll < pack.BronzeChance + pack.SilverChance + pack.GoldChance + pack.PlatinumChance) rarity = Rarity.Platinum;
                else rarity = Rarity.Diamond;

                // Ajusta as estatísticas com base na raridade
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

                await _context.UserPokemons.AddAsync(userPokemon);  // Adiciona o Pokémon do utilizador ao contexto

                // Garante que há um registo de TotalPacksOpenedRanking para o utilizador
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

                // Garante que há um registo de TotalDiamondPokemonsRanking para o utilizador
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

                // Salva o histórico de packs abertos pelo utilizador
                var packUser = new PackUsers
                {
                    UserId = userId,
                    PackId = packId,
                    OpenedOn = DateTime.UtcNow
                };

                await _context.PackUsers.AddAsync(packUser);

                // Atualiza o total de packs comprados
                pack.TotalBought += 1;
                _context.Packs.Update(pack);

                // Atualiza o NextOpenExpected do utilizador se for um pack gratuito
                if (isPackFree)
                {
                    user.NextOpenExpected = DateTime.UtcNow.AddMinutes(3); // Exemplo: próximo pack gratuito em 3 minutos
                    _context.Users.Update(user);
                }

                // Salva as alterações na base de dados
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Retorna o resultado
                if (isPackFree)
                {
                    return Ok(new { Pokemon = selectedPokemon.Name, Rarity = rarity.ToString(), Image = selectedPokemon.Image != null ? Convert.ToBase64String(selectedPokemon.Image) : null, NextOpenExpected = user.NextOpenExpected });
                }
                else
                {
                    return Ok(new { Pokemon = selectedPokemon.Name, Rarity = rarity.ToString(), Image = selectedPokemon.Image != null ? Convert.ToBase64String(selectedPokemon.Image) : null });
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await transaction.RollbackAsync();  // Reverte a transação em caso de erro de concorrência
                return Conflict(new { Message = "A concurrency error occurred. Please try again.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();  // Reverte a transação em caso de erro
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllOwnedPokemonsWithFiltersWithPaginationAndSearch")]  // Define um endpoint HTTP GET na rota "GetAllOwnedPokemonsWithFiltersWithPaginationAndSearch"
        public async Task<IActionResult> GetAllOwnedPokemonsWithFiltersWithPaginationAndSearch(  // Método para obter todos os Pokémons do utilizador com filtros, paginação e pesquisa
            int userId, int page, int maxRecords, bool isLatest,
            string? rarityChoosen,  // Tornando opcional
            bool isMostAttackSelected, bool isMostHPSelected,
            bool isMostDefSelected, bool isMostSpeedSelected,
            string searchKeyword = "")
        {
            var query = _context.UserPokemons
                .Where(up => up.UserId == userId)  // Filtra os Pokémons pelo ID do utilizador
                .Join(_context.Pokemons,  // Junta os Pokémons do utilizador com os Pokémons
                      up => up.PokemonId,
                      p => p.Id,
                      (up, p) => new { up, p })
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchKeyword))  // Se a palavra-chave de pesquisa não estiver vazia
            {
                searchKeyword = searchKeyword.Trim().ToLower();  // Remove espaços em branco e converte para minúsculas
                query = query.Where(up => up.p.Name.ToLower().Contains(searchKeyword));  // Filtra os Pokémons pelo nome
            }

            if (!string.IsNullOrEmpty(rarityChoosen))  // Se a raridade escolhida não estiver vazia
            {
                rarityChoosen = rarityChoosen.ToLower();
                query = query.Where(up => up.up.Rarity.ToLower() == rarityChoosen);  // Filtra os Pokémons pela raridade
            }

            if (isMostAttackSelected)
            {
                query = query.OrderByDescending(up => up.up.ActualAttackPoints);  // Ordena por pontos de ataque em ordem decrescente
            }
            else if (isMostHPSelected)
            {
                query = query.OrderByDescending(up => up.up.ActualHealthPoints);  // Ordena por pontos de vida em ordem decrescente
            }
            else if (isMostDefSelected)
            {
                query = query.OrderByDescending(up => up.up.ActualDefensePoints);  // Ordena por pontos de defesa em ordem decrescente
            }
            else if (isMostSpeedSelected)
            {
                query = query.OrderByDescending(up => up.up.ActualSpeedPoints);  // Ordena por pontos de velocidade em ordem decrescente
            }
            else if (isLatest)
            {
                query = query.OrderByDescending(up => up.up.CreatedOn);  // Ordena pela data de criação em ordem decrescente
            }
            else
            {
                query = query.OrderBy(up => up.up.CreatedOn);  // Ordena pela data de criação em ordem crescente
            }

            var totalPokemons = await query.CountAsync();  // Conta o total de Pokémons

            var pokemons = await query
                .Skip((page - 1) * maxRecords)  // Pula os Pokémons das páginas anteriores
                .Take(maxRecords)  // Toma o número máximo de Pokémons para a página atual
                .Select(up => new UserPokemonDTO  // Projeta os resultados para o DTO
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
                    UpdatedBy = up.up.UpdatedBy,
                    Image = up.p.Image != null ? Convert.ToBase64String(up.p.Image) : null  // Adiciona a imagem codificada em base64
                })
                .ToListAsync();  // Converte o resultado para uma lista de forma assíncrona

            return Ok(new { totalPokemons, pokemons });  // Retorna o total de Pokémons e os Pokémons da página atual
        }
    }
}
