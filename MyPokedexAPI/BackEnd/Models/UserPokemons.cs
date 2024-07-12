using System;
using System.ComponentModel.DataAnnotations;

 public class UserPokemons
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PokemonId { get; set; }
    public int ActualAttackPoints { get; set; }
    public int ActualHealthPoints { get; set; }
    public int ActualDefensePoints { get; set; }
    public int ActualSpeedPoints { get; set; }
    public int TotalCombatPoints { get; set; }
    public int Rarity { get; set; }
    public int UserPackId { get; set; }
    public bool IsFavourite { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    
    // Propriedades de navegação
    public User CreatedBy { get; set; }
    public User UpdatedBy { get; set; }
    public User User { get; set; }
    public Pokemon Pokemon { get; set; }
    public PackUsers PackUser { get; set; }
}

