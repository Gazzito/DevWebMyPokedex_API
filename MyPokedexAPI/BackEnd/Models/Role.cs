using System;  // Importa o namespace para funcionalidades básicas do sistema
using System.ComponentModel.DataAnnotations;  // Importa o namespace para validação de dados
using System.ComponentModel.DataAnnotations.Schema;  // Importa o namespace para mapeamento de dados com o banco de dados

namespace MyPokedexAPI.Models  // Define o namespace para os modelos da aplicação
{
    public class Role  // Define a classe Role que representa um papel no sistema
    {
        [Key]  // Indica que esta propriedade é a chave primária
        public int Id { get; set; }

        [Required]  // Indica que esta propriedade é obrigatória
        [StringLength(100)]  // Define o tamanho máximo da string
        public string Name { get; set; }

        [Required]  // Indica que esta propriedade é obrigatória
        public DateTime CreatedOn { get; set; }  // Define a data de criação do papel

        public int? CreatedBy { get; set; }  // Define o ID do utilizador que criou o papel

        public DateTime? UpdatedOn { get; set; }  // Define a data de atualização do papel

        public int? UpdatedBy { get; set; }  // Define o ID do utilizador que atualizou o papel

        // Navegação para o utilizador que criou este role
        [ForeignKey("CreatedBy")]
        public virtual User CreatedByUser { get; set; }  // Define a relação com a entidade User para o criador do papel

        // Navegação para o utilizador que atualizou este role
        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedByUser { get; set; }  // Define a relação com a entidade User para o atualizador do papel
    }

    public class RoleDTO  // Define a classe RoleDTO que representa o Data Transfer Object para Role
    {
        [Required]
        public int Id { get; set; }  // Define o ID do papel

        [Required]
        [StringLength(50)]  // Define o tamanho máximo da string
        public string Name { get; set; }  // Define o nome do papel

        public int? CreatedBy { get; set; }  // Define o ID do utilizador que criou o papel

        public DateTime? UpdatedOn { get; set; }  // Define a data de atualização do papel

        public int? UpdatedBy { get; set; }  // Define o ID do utilizador que atualizou o papel
    }
}
