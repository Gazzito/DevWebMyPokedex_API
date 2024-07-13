// User.cs (Model)

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MyPokedexAPI.Models{
public class User
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Username { get; set; }
  public string Password { get; set; }
  public string Email { get; set; }
  public string MobilePhone { get; set; }
  public DateTime Creation_Date { get; set; }
  public DateTime Last_Login { get; set; }
  public bool Is_Active { get; set; }


  // Navigation properties
  public ICollection<Pokemon> CreatedPokemons { get; set; }
  public ICollection<Pokemon> UpdatedPokemons { get; set; }
  public ICollection<Region> CreatedRegions { get; set; }
  public ICollection<Region> UpdatedRegions { get; set; }
  public ICollection<UserRole> UserRoles { get; set; }

  public ICollection<UserProfile> UserProfiles { get; set; }
  public ICollection<TotalDiamondPokemonsRanking> TotalDiamondPokemonsRankings { get; set; }
  public ICollection<TotalPacksOpenedRanking> TotalPacksOpenedRankings { get; set; }
  public ICollection<UserPokemon> UserPokemons { get; set; }
  public ICollection<Transaction> Transactions { get; set; }
  public ICollection<PackUsers> PackUsers { get; set; }

         public ICollection<PokemonInPack> PokemonInPacksCreated { get; set; }
        public ICollection<PokemonInPack> PokemonInPacksUpdated { get; set; }


         public User()
        {
            CreatedPokemons = new List<Pokemon>();
            UpdatedPokemons = new List<Pokemon>();
            CreatedRegions = new List<Region>();
            UpdatedRegions = new List<Region>();
            UserRoles = new List<UserRole>();
            UserProfiles = new List<UserProfile>();
            TotalDiamondPokemonsRankings = new List<TotalDiamondPokemonsRanking>();
            TotalPacksOpenedRankings = new List<TotalPacksOpenedRanking>();
            UserPokemons = new List<UserPokemon>();
            Transactions = new List<Transaction>();
            PackUsers = new List<PackUsers>();
            PokemonInPacksCreated = new List<PokemonInPack>();
            PokemonInPacksUpdated = new List<PokemonInPack>();
        }
  
}

 public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public DateTime Creation_Date { get; set; }
        public DateTime Last_Login { get; set; }
        public bool Is_Active { get; set; }
    }

}