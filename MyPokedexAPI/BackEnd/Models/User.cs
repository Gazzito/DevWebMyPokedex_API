using System;  // Importa o namespace para funcionalidades básicas do sistema
using System.Collections.Generic;  // Importa o namespace para coleções genéricas
using System.ComponentModel.DataAnnotations;  // Importa o namespace para validação de dados

namespace MyPokedexAPI.Models  // Define o namespace para os modelos da aplicação
{
    public class User  // Define a classe User que representa um utilizador no sistema
    {
        [Key]  // Indica que esta propriedade é a chave primária
        public int Id { get; set; }

        [Required]  // Indica que esta propriedade é obrigatória
        [StringLength(100)]  // Define o tamanho máximo da string
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]  // Indica que esta propriedade deve ser um endereço de e-mail válido
        [StringLength(100)]
        public string Email { get; set; }

        public DateTime? NextOpenExpected { get; set; }  // Define a próxima data esperada para abrir algo (por exemplo, um pack)

        [Required]
        public DateTime CreationDate { get; set; }  // Define a data de criação do utilizador

        public DateTime? LastLogin { get; set; }  // Define a última data de login do utilizador

        [Required]
        public bool IsActive { get; set; }  // Indica se o utilizador está ativo

        // Propriedades de navegação para relacionamentos com outras entidades
        public virtual UserProfile UserProfile { get; set; }

        public virtual TotalDiamondPokemonsRanking TotalDiamondPokemonsRanking { get; set; }

        public virtual TotalPacksOpenedRanking TotalPacksOpenedRanking { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<PackUsers> PackUsers { get; set; } 

        public virtual ICollection<UserPokemons> UserPokemons { get; set; }

        public virtual ICollection<UserPokemons> UserPokemonsCreatedBy { get; set; } 

        public virtual ICollection<UserPokemons> UserPokemonsUpdatedBy { get; set; }
    }

    public class UserDTO  // Define a classe UserDTO que representa o Data Transfer Object para User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public DateTime? NextOpenExpected { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public DateTime? LastLogin { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }

    public class UserRegisterDTO  // Define a classe UserRegisterDTO para registrar um novo utilizador
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }
    }

    public class UserLoginDTO  // Define a classe UserLoginDTO para login de um utilizador
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }
    }
}
