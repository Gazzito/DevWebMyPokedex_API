using System;  // Importa o namespace para funcionalidades básicas do sistema
using System.Collections.Generic;  // Importa o namespace para coleções genéricas
using System.ComponentModel.DataAnnotations;  // Importa o namespace para validação de dados
using System.ComponentModel.DataAnnotations.Schema;  // Importa o namespace para mapeamento de dados com o banco de dados

namespace MyPokedexAPI.Models  // Define o namespace para os modelos da aplicação
{
    public class Pokemon  // Define a classe Pokemon que representa um Pokémon no sistema
    {
        [Key]  // Indica que esta propriedade é a chave primária
        public int Id { get; set; }

        [Required]  // Indica que esta propriedade é obrigatória
        [StringLength(100)]  // Define o tamanho máximo da string
        public string Name { get; set; }

        [Required]
        public int RegionId { get; set; }  // Define a chave estrangeira para a região

        [Required]
        public int BaseAttackPoints { get; set; }  // Define os pontos de ataque base

        [Required]
        public int BaseHealthPoints { get; set; }  // Define os pontos de saúde base

        [Required]
        public int BaseDefensePoints { get; set; }  // Define os pontos de defesa base

        [Required]
        public int BaseSpeedPoints { get; set; }  // Define os pontos de velocidade base

        [Required]
        public DateTime CreatedOn { get; set; }  // Define a data de criação do Pokémon

        [Required]
        public int CreatedBy { get; set; }  // Define o ID do usuário que criou o Pokémon

        public DateTime? UpdatedOn { get; set; }  // Define a data de atualização do Pokémon

        public int? UpdatedBy { get; set; }  // Define o ID do usuário que atualizou o Pokémon

        public byte[]? Image { get; set; }  // Define a imagem do Pokémon como um array de bytes

        // Navegação para a região associada
        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; }  // Define a relação com a entidade Region

        // Navegação para o utilizador que criou este Pokémon
        [ForeignKey("CreatedBy")]
        public virtual User CreatedByUser { get; set; }  // Define a relação com a entidade User para o criador do Pokémon

        // Navegação para o usuário que atualizou este Pokémon
        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedByUser { get; set; }  // Define a relação com a entidade User para o atualizador do Pokémon

        public virtual ICollection<PokemonInPack> PokemonInPacks { get; set; }  // Define a coleção de PokemonInPacks associada ao Pokémon

        public virtual ICollection<UserPokemons> UserPokemons { get; set; }  // Define a coleção de UserPokemons associada ao Pokémon
    }

    public class PokemonDTO  // Define a classe PokemonDTO que representa o Data Transfer Object para Pokemon
    {
        [Required]
        public int Id { get; set; }  // Define o ID do Pokémon

        [Required]
        [StringLength(100)]
        public string Name { get; set; }  // Define o nome do Pokémon

        [Required]
        public int RegionId { get; set; }  // Define a chave estrangeira para a região

        [Required]
        public int BaseAttackPoints { get; set; }  // Define os pontos de ataque base

        [Required]
        public int BaseHealthPoints { get; set; }  // Define os pontos de saúde base

        [Required]
        public int BaseDefensePoints { get; set; }  // Define os pontos de defesa base

        [Required]
        public int BaseSpeedPoints { get; set; }  // Define os pontos de velocidade base

        public string? Image { get; set; }  // Define a imagem do Pokémon como uma string base64

        public DateTime CreatedOn { get; set; }  // Define a data de criação do Pokémon

        public int CreatedBy { get; set; }  // Define o ID do usuário que criou o Pokémon

        public DateTime? UpdatedOn { get; set; }  // Define a data de atualização do Pokémon

        public int? UpdatedBy { get; set; }  // Define o ID do usuário que atualizou o Pokémon
    }
}
