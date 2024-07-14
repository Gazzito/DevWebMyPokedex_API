using System;  // Importa o namespace para funcionalidades básicas do sistema
using System.Collections.Generic;  // Importa o namespace para coleções genéricas
using System.ComponentModel.DataAnnotations;  // Importa o namespace para validação de dados
using System.ComponentModel.DataAnnotations.Schema;  // Importa o namespace para mapeamento de dados com o banco de dados

namespace MyPokedexAPI.Models  // Define o namespace para os modelos da aplicação
{
    public class Pack  // Define a classe Pack que representa um pacote no sistema
    {
        [Key]  // Indica que esta propriedade é a chave primária
        public int Id { get; set; }

        [Required]  // Indica que esta propriedade é obrigatória
        [StringLength(100)]  // Define o tamanho máximo da string
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }  // Define o preço do pacote

        [Required]
        public byte[]? Image { get; set; }  // Define a imagem do pacote como um array de bytes

        [Required]
        public double BronzeChance { get; set; }  // Define a chance de obter um item de bronze

        [Required]
        public double SilverChance { get; set; }  // Define a chance de obter um item de prata

        [Required]
        public double GoldChance { get; set; }  // Define a chance de obter um item de ouro

        [Required]
        public double PlatinumChance { get; set; }  // Define a chance de obter um item de platina

        [Required]
        public double DiamondChance { get; set; }  // Define a chance de obter um item de diamante

        [Required]
        public int TotalBought { get; set; }  // Define o total de pacotes comprados

        [Required]
        public DateTime CreatedOn { get; set; }  // Define a data de criação do pacote

        [Required]
        public int CreatedBy { get; set; }  // Define o ID do usuário que criou o pacote

        public DateTime? UpdatedOn { get; set; }  // Define a data de atualização do pacote

        public int? UpdatedBy { get; set; }  // Define o ID do usuário que atualizou o pacote

        // Navegação para o utilizador que criou este pack
        [ForeignKey("CreatedBy")]
        public virtual User CreatedByUser { get; set; }  // Define a relação com a entidade User para o criador do pacote

        // Navegação para o utilizador que atualizou este pack
        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedByUser { get; set; }  // Define a relação com a entidade User para o atualizador do pacote

        // Coleção de UserPokemons
        public virtual ICollection<UserPokemons> UserPokemons { get; set; }  // Define a coleção de UserPokemons associada ao pacote

        // Coleção de PackUsers
        public virtual ICollection<PackUsers> PackUsers { get; set; }  // Define a coleção de PackUsers associada ao pacote

        // Coleção de PokemonInPacks
        public virtual ICollection<PokemonInPack> PokemonInPacks { get; set; }  // Define a coleção de PokemonInPacks associada ao pacote
    }

    public class PackDTO  // Define a classe PackDTO que representa o Data Transfer Object para Pack
    {
        [Required]
        public int Id { get; set; }  // Define o ID do pacote

        [Required]
        [StringLength(100)]
        public string Name { get; set; }  // Define o nome do pacote

        [Required]
        public decimal Price { get; set; }  // Define o preço do pacote

        public string? Image { get; set; }  // Define a imagem do pacote como uma string base64

        [Required]
        public double BronzeChance { get; set; }  // Define a chance de obter um item de bronze

        [Required]
        public double SilverChance { get; set; }  // Define a chance de obter um item de prata

        [Required]
        public double GoldChance { get; set; }  // Define a chance de obter um item de ouro

        [Required]
        public double PlatinumChance { get; set; }  // Define a chance de obter um item de platina

        [Required]
        public double DiamondChance { get; set; }  // Define a chance de obter um item de diamante

        [Required]
        public int TotalBought { get; set; }  // Define o total de pacotes comprados

        [Required]
        public DateTime CreatedOn { get; set; }  // Define a data de criação do pacote

        [Required]
        public int CreatedBy { get; set; }  // Define o ID do usuário que criou o pacote

        public DateTime? UpdatedOn { get; set; }  // Define a data de atualização do pacote

        public int? UpdatedBy { get; set; }  // Define o ID do usuário que atualizou o pacote

        // Coleção de UserPokemons
        public virtual ICollection<UserPokemons>? UserPokemons { get; set; }  // Define a coleção de UserPokemons associada ao pacote
    }
}
