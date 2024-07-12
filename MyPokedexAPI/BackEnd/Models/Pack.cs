using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class Pack
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public byte[] Image { get; set; }
    public decimal BronzeChance { get; set; }
    public decimal SilverChance { get; set; }
    public decimal GoldChance { get; set; }
    public decimal PlatinumChance { get; set; }
    public decimal DiamondChance { get; set; }
    public string TotalBought { get; set; }
    public DateTime CreatedOn { get; set; }
    public User CreateBy { get; set; }
    public DateTime  UpdatedOn { get; set; }
    public User  UpdatedBy { get; set; }

  
       // Navigation properties
        public ICollection<PokemonInPack> PokemonInPacks { get; set; }
        public ICollection<PackUsers> PackUsers { get; set; }
}
