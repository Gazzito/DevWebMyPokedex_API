using System;  // Importa o namespace para funcionalidades básicas do sistema
using System.ComponentModel.DataAnnotations;  // Importa o namespace para validação de dados
using System.ComponentModel.DataAnnotations.Schema;  // Importa o namespace para mapeamento de dados com o banco de dados

namespace MyPokedexAPI.Models  // Define o namespace para os modelos da aplicação
{
    public class UserPokemons  // Define a classe UserPokemons que representa a relação entre um utilizador e um Pokémon no sistema
    {
        [Key]  // Indica que esta propriedade é a chave primária
        public int Id { get; set; }

        [Required]  // Indica que esta propriedade é obrigatória
        public int UserId { get; set; }  // Define a chave estrangeira para o utilizador

        [Required]
        public int PokemonId { get; set; }  // Define a chave estrangeira para o Pokémon

        [Required]
        public int ActualAttackPoints { get; set; }  // Define os pontos de ataque atuais

        [Required]
        public int ActualHealthPoints { get; set; }  // Define os pontos de saúde atuais

        [Required]
        public int ActualDefensePoints { get; set; }  // Define os pontos de defesa atuais

        [Required]
        public int ActualSpeedPoints { get; set; }  // Define os pontos de velocidade atuais

        [Required]
        public int TotalCombatPoints { get; set; }  // Define os pontos de combate totais

        [Required]
        public string Rarity { get; set; }  // Define a raridade do Pokémon

        [Required]
        public int PackId { get; set; }  // Define a chave estrangeira para o pack

        [Required]
        public bool IsFavourite { get; set; }  // Indica se o Pokémon é favorito

        [Required]
        public DateTime CreatedOn { get; set; }  // Define a data de criação do registro

        [Required]
        public int CreatedBy { get; set; }  // Define o ID do utilizador que criou o registro

        public DateTime? UpdatedOn { get; set; }  // Define a data de atualização do registro

        public int? UpdatedBy { get; set; }  // Define o ID do utilizador que atualizou o registro

        // Propriedades de navegação para relacionamentos com outras entidades
        [ForeignKey("UserId")]
        public virtual User User { get; set; }  // Define a relação com a entidade User

        [ForeignKey("PokemonId")]
        public virtual Pokemon Pokemon { get; set; }  // Define a relação com a entidade Pokemon

        [ForeignKey("PackId")]
        public virtual Pack Pack { get; set; }  // Define a relação com a entidade Pack

        [ForeignKey("CreatedBy")]
        public virtual User CreatedByUser { get; set; }  // Define a relação com a entidade User para o criador do registro

        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedByUser { get; set; }  // Define a relação com a entidade User para o atualizador do registro
    }

    public class UserPokemonDTO  // Define a classe UserPokemonDTO que representa o Data Transfer Object para UserPokemons
    {
        public int Id { get; set; }  // Define o ID do registro
        public int UserId { get; set; }  // Define a chave estrangeira para o utilizador
        public int PokemonId { get; set; }  // Define a chave estrangeira para o Pokémon
        public string PokemonName { get; set; }  // Define o nome do Pokémon
        public int ActualAttackPoints { get; set; }  // Define os pontos de ataque atuais
        public int ActualHealthPoints { get; set; }  // Define os pontos de saúde atuais
        public int ActualDefensePoints { get; set; }  // Define os pontos de defesa atuais
        public int ActualSpeedPoints { get; set; }  // Define os pontos de velocidade atuais
        public int TotalCombatPoints { get; set; }  // Define os pontos de combate totais
        public string Rarity { get; set; }  // Define a raridade do Pokémon
        public int PackId { get; set; }  // Define a chave estrangeira para o pack
        public bool IsFavourite { get; set; }  // Indica se o Pokémon é favorito
        public DateTime CreatedOn { get; set; }  // Define a data de criação do registro
        public int CreatedBy { get; set; }  // Define o ID do utilizador que criou o registro
        public DateTime? UpdatedOn { get; set; }  // Define a data de atualização do registro
        public int? UpdatedBy { get; set; }  // Define o ID do utilizador que atualizou o registro
        public string Image { get; set; }  // Define a imagem do Pokémon como uma string base64
    }
}
