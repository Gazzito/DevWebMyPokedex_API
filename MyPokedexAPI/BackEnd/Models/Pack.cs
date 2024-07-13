using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPokedexAPI.Models{

public class Pack
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
        public double BronzeChance { get; set; }
        public double SilverChance { get; set; }
        public double GoldChance { get; set; }
        public double PlatinumChance { get; set; }
        public double DiamondChance { get; set; }
        public int TotalBought { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedById { get; set; }

        // Navigation properties
        public User CreatedBy { get; set; }
        public User UpdatedBy { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<PackUsers> PackUsers { get; set; }

        public ICollection<PokemonInPack> PokemonInPacks { get; set; }
    }

   public class PackDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageBase64 { get; set; }  // String para receber a imagem em base64
        public double BronzeChance { get; set; }
        public double SilverChance { get; set; }
        public double GoldChance { get; set; }
        public double PlatinumChance { get; set; }
        public double DiamondChance { get; set; }
        public int TotalBought { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedById { get; set; }
    }
}