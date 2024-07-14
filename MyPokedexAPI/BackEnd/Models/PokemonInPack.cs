using System;  // Importa o namespace para funcionalidades básicas do sistema
using System.ComponentModel.DataAnnotations;  // Importa o namespace para validação de dados
using System.ComponentModel.DataAnnotations.Schema;  // Importa o namespace para mapeamento de dados com o banco de dados

namespace MyPokedexAPI.Models  // Define o namespace para os modelos da aplicação
{
    public class PokemonInPack  // Define a classe PokemonInPack que representa a relação entre um Pokémon e um pack no sistema
    {
        [Key]  // Indica que esta propriedade é a chave primária
        public int Id { get; set; }

        [Required]  // Indica que esta propriedade é obrigatória
        public int PackId { get; set; }  // Define a chave estrangeira para o pack

        [Required]  // Indica que esta propriedade é obrigatória
        public int PokemonId { get; set; }  // Define a chave estrangeira para o Pokémon

        [Required]  // Indica que esta propriedade é obrigatória
        public DateTime CreatedOn { get; set; }  // Define a data de criação do registro

        [Required]  // Indica que esta propriedade é obrigatória
        public int CreatedBy { get; set; }  // Define o ID do usuário que criou o registro

        public DateTime? UpdatedOn { get; set; }  // Define a data de atualização do registro

        public int? UpdatedBy { get; set; }  // Define o ID do usuário que atualizou o registro

        // Navegação para o pack associado
        [ForeignKey("PackId")]
        public virtual Pack Pack { get; set; }  // Define a relação com a entidade Pack

        // Navegação para o Pokémon associado
        [ForeignKey("PokemonId")]
        public virtual Pokemon Pokemon { get; set; }  // Define a relação com a entidade Pokemon

        // Navegação para o utilizador que criou este registro
        [ForeignKey("CreatedBy")]
        public virtual User CreatedByUser { get; set; }  // Define a relação com a entidade User para o criador do registro

        // Navegação para o utilizador que atualizou este registro
        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedByUser { get; set; }  // Define a relação com a entidade User para o atualizador do registro
    }

    public class PokemonInPackDTO  // Define a classe PokemonInPackDTO que representa o Data Transfer Object para PokemonInPack
    {
        [Required]
        public int Id { get; set; }  // Define o ID do registro

        [Required]
        public int PackId { get; set; }  // Define a chave estrangeira para o pack

        [Required]
        public int PokemonId { get; set; }  // Define a chave estrangeira para o Pokémon

        [Required]
        public DateTime CreatedOn { get; set; }  // Define a data de criação do registro

        [Required]
        public int CreatedBy { get; set; }  // Define o ID do usuário que criou o registro

        public DateTime? UpdatedOn { get; set; }  // Define a data de atualização do registro

        public int? UpdatedBy { get; set; }  // Define o ID do usuário que atualizou o registro
    }
}
