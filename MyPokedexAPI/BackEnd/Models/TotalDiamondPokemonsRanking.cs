using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPokedexAPI.Models
{
    public class TotalDiamondPokemonsRanking
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        public int TotalDiamondPokemons { get; set; }

        [Required]
        public int Rank { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        // Navegação para o utilizador que criou este ranking
        [ForeignKey("CreatedBy")]
        public virtual User CreatedByUser { get; set; }

        // Navegação para o utilizador que atualizou este ranking
        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedByUser { get; set; }
        public virtual User User { get; set; }
    }
}
