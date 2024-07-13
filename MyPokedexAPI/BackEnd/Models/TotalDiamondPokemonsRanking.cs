using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPokedexAPI.Models{
    public class TotalDiamondPokemonsRanking
     {
        public int Id { get; set; }
        public int TotalDiamondPokemons { get; set; }
        public int Rank { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedById { get; set; }

        // Navigation properties
        public User CreatedBy { get; set; }
        public User UpdatedBy { get; set; }
    }
}