using System;  // Importa o namespace para funcionalidades básicas do sistema
using System.ComponentModel.DataAnnotations;  // Importa o namespace para validação de dados
using System.ComponentModel.DataAnnotations.Schema;  // Importa o namespace para mapeamento de dados com o banco de dados

namespace MyPokedexAPI.Models  // Define o namespace para os modelos da aplicação
{
    public class Region  // Define a classe Region que representa uma região no sistema
    {
        [Key]  // Indica que esta propriedade é a chave primária
        public int Id { get; set; }

        [Required]  // Indica que esta propriedade é obrigatória
        [StringLength(100)]  // Define o tamanho máximo da string
        public string Name { get; set; }

        [Required]  // Indica que esta propriedade é obrigatória
        public DateTime CreatedOn { get; set; }  // Define a data de criação da região

        [Required]  // Indica que esta propriedade é obrigatória
        public int CreatedBy { get; set; }  // Define o ID do utilizador que criou a região

        public DateTime? UpdatedOn { get; set; }  // Define a data de atualização da região

        public int? UpdatedBy { get; set; }  // Define o ID do utilizador que atualizou a região

        // Navegação para o utilizador que criou esta região
        [ForeignKey("CreatedBy")]
        public virtual User CreatedByUser { get; set; }  // Define a relação com a entidade User para o criador da região

        // Navegação para o utilizador que atualizou esta região
        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedByUser { get; set; }  // Define a relação com a entidade User para o atualizador da região
    }

    public class RegionDTO  // Define a classe RegionDTO que representa o Data Transfer Object para Region
    {
        [Required]
        public int Id { get; set; }  // Define o ID da região

        [Required]
        [StringLength(100)]
        public string Name { get; set; }  // Define o nome da região

        [Required]
        public DateTime CreatedOn { get; set; }  // Define a data de criação da região

        [Required]
        public int CreatedBy { get; set; }  // Define o ID do utilizador que criou a região

        public DateTime? UpdatedOn { get; set; }  // Define a data de atualização da região

        public int? UpdatedBy { get; set; }  // Define o ID do utilizador que atualizou a região
    }
}
