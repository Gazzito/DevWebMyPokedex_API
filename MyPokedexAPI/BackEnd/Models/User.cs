using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPokedexAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;
 
        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public DateTime CreationDate { get; set; }

        public DateTime? LastLogin { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public virtual UserProfile UserProfile { get; set; } = new UserProfile();

        public virtual TotalDiamondPokemonsRanking TotalDiamondPokemonsRanking { get; set; } = new TotalDiamondPokemonsRanking();

        public virtual TotalPacksOpenedRanking TotalPacksOpenedRanking { get; set; } = new TotalPacksOpenedRanking();

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public virtual ICollection<PackUsers> PackUsers { get; set; } = new List<PackUsers>();

        public virtual ICollection<UserPokemons> UserPokemons { get; set; } = new List<UserPokemons>();

         public virtual ICollection<UserPokemons> UserPokemonsCreatedBy { get; set; } = new List<UserPokemons>();
         public virtual ICollection<UserPokemons> UserPokemonsUpdatedBy { get; set; } = new List<UserPokemons>();
    }


     public class UserDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty; 

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty; 

        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty; 

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty; 

        [Required]
        public DateTime CreationDate { get; set; }

        public DateTime? LastLogin { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
