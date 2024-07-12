using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

  public class PokemonInPack
    {
        public int Id { get; set; }
        public int PackId { get; set; }
        public int PokemonId { get; set; }
        public DateTime CreatedOn { get; set; }
        public User CreatedBy{ get; set; }
        public DateTime UpdatedOn { get; set; }

        // Navigation properties
        public Pack Pack { get; set; }
        public Pokemon Pokemon { get; set; }
    }