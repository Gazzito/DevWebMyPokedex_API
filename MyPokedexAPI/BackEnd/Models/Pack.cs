using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPokedexAPI.Models
{
    public class Pack
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public byte[]? Image { get; set; } = Array.Empty<byte>();

        [Required]
        public double BronzeChance { get; set; }

        [Required]
        public double SilverChance { get; set; }

        [Required]
        public double GoldChance { get; set; }

        [Required]
        public double PlatinumChance { get; set; }

        [Required]
        public double DiamondChance { get; set; }

        [Required]
        public int TotalBought { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        // Navegação para o utilizador que criou este pack
        [ForeignKey("CreatedBy")]
        public virtual User CreatedByUser { get; set; } = new User();

        // Navegação para o utilizador que atualizou este pack
        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedByUser { get; set; } = new User();

        // Coleção de UserPokemons
        public virtual ICollection<UserPokemons> UserPokemons { get; set; } = new List<UserPokemons>();

        // Coleção de PackUsers
        public virtual ICollection<PackUsers> PackUsers { get; set; } = new List<PackUsers>();

        // Coleção de PokemonInPacks
        public virtual ICollection<PokemonInPack> PokemonInPacks { get; set; } = new List<PokemonInPack>();
    }

     public class PackDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        public string? Image { get; set; } = string.Empty;

        [Required]
        public double BronzeChance { get; set; }

        [Required]
        public double SilverChance { get; set; }

        [Required]
        public double GoldChance { get; set; }

        [Required]
        public double PlatinumChance { get; set; }

        [Required]
        public double DiamondChance { get; set; }

        [Required]
        public int TotalBought { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        // Coleção de UserPokemons
        public virtual ICollection<UserPokemons> UserPokemons { get; set; } = new List<UserPokemons>();
    }
}
