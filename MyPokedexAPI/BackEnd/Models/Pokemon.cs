using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPokedexAPI.Models
{
    public class Pokemon
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int RegionId { get; set; }

        [Required]
        public int BaseAttackPoints { get; set; }

        [Required]
        public int BaseHealthPoints { get; set; }

        [Required]
        public int BaseDefensePoints { get; set; }

        [Required]
        public int BaseSpeedPoints { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public byte[]? Image { get; set; } = Array.Empty<byte>();
        // Navegação para a região associada
        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; } = new Region();

        // Navegação para o utilizador que criou este Pokémon
        [ForeignKey("CreatedBy")]
        public virtual User CreatedByUser { get; set; } = new User();

        // Navegação para o usuário que atualizou este Pokémon
        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedByUser { get; set; } = new User();

        public virtual ICollection<PokemonInPack> PokemonInPacks { get; set; } = new List<PokemonInPack>();

        public virtual ICollection<UserPokemons> UserPokemons { get; set; } = new List<UserPokemons>();
    }

public class PokemonDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty; 

        [Required]
        public int RegionId { get; set; }

        [Required]
        public int BaseAttackPoints { get; set; }

        [Required]
        public int BaseHealthPoints { get; set; }

        [Required]
        public int BaseDefensePoints { get; set; }

        [Required]
        public int BaseSpeedPoints { get; set; }

        public string? Image { get; set; } = string.Empty; 

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }
    }
}
