using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace MyPokedexAPI.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedById { get; set; }

        // Navigation property
        public ICollection<Pokemon> Pokemons { get; set; } = new List<Pokemon>();
        public User CreatedBy { get; set; }
        public User UpdatedBy { get; set; }

    }
    public class RegionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedById { get; set; }
    }


}
