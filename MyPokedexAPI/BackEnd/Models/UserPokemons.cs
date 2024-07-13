using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPokedexAPI.Models{


    public class UserPokemon
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PokemonId { get; set; }
        public int ActualAttackPoints { get; set; }
        public int ActualHealthPoints { get; set; }
        public int ActualDefensePoints { get; set; }
        public int ActualSpeedPoints { get; set; }
        public int TotalCombatPoints { get; set; }
        public Rarity Rarity { get; set; }
        public int UserPackId { get; set; }
        public bool IsFavourite { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedById { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Pokemon Pokemon { get; set; }
        public User CreatedBy { get; set; }
        public User UpdatedBy { get; set; }
    }

      public class UserPokemonDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PokemonId { get; set; }
        public int ActualAttackPoints { get; set; }
        public int ActualHealthPoints { get; set; }
        public int ActualDefensePoints { get; set; }
        public int ActualSpeedPoints { get; set; }
        public int TotalCombatPoints { get; set; }
        public Rarity Rarity { get; set; }
        public int UserPackId { get; set; }
        public bool IsFavourite { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedById { get; set; }
    }
}