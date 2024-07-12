// User.cs (Model)

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
    public bool  Is_Active { get; set; }
    
   // Navigation properties
     public ICollection<User_Role> User_Roles { get; set; }
        public ICollection<UserProfile> CreatedUserProfiles { get; set; }
        public ICollection<UserProfile> UpdatedUserProfiles { get; set; }
        public ICollection<UserPokemons> CreatedUserPokemons { get; set; }
        public ICollection<UserPokemons> UpdatedUserPokemons { get; set; }
        public ICollection<Region> CreatedRegions { get; set; }
        public ICollection<Region> UpdatedRegions { get; set; }
        public ICollection<Pokemon> CreatedPokemons { get; set; }
        public ICollection<Pokemon> UpdatedPokemons { get; set; }
        public ICollection<PackUsers> PackUsers { get; set; }
        public ICollection<TotalPacksOpenedRanking> CreatedTotalPacksOpenedRankings { get; set; }
        public ICollection<TotalPacksOpenedRanking> UpdatedTotalPacksOpenedRankings { get; set; }
        public ICollection<TotalDiamondPokemonsRanking> CreatedTotalDiamondPokemonsRankings { get; set; }
        public ICollection<TotalDiamondPokemonsRanking> UpdatedTotalDiamondPokemonsRankings { get; set; }

  
}
