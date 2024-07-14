using System;  // Importa o namespace para funcionalidades básicas do sistema
using System.ComponentModel.DataAnnotations;  // Importa o namespace para validação de dados
using System.ComponentModel.DataAnnotations.Schema;  // Importa o namespace para mapeamento de dados com o banco de dados

namespace MyPokedexAPI.Models  // Define o namespace para os modelos da aplicação
{
    public class UserProfile  // Define a classe UserProfile que representa o perfil de um utilizador no sistema
    {
        [Key]  // Indica que esta propriedade é a chave primária
        [ForeignKey("User")]  // Indica que esta propriedade é uma chave estrangeira para a entidade User
        public int Id { get; set; }

        [Required]  // Indica que esta propriedade é obrigatória
        public decimal Money { get; set; }  // Define o dinheiro associado ao perfil do utilizador

        [Required]
        [StringLength(200)]  // Define o tamanho máximo da string
        public string FullName { get; set; }  // Define o nome completo do utilizador

        [Required]
        public DateTime CreatedOn { get; set; }  // Define a data de criação do perfil

        [Required]
        public int CreatedBy { get; set; }  // Define o ID do utilizador que criou o perfil

        public DateTime? UpdatedOn { get; set; }  // Define a data de atualização do perfil

        public int? UpdatedBy { get; set; }  // Define o ID do utilizador que atualizou o perfil

        // Propriedades de navegação para relacionamentos com outras entidades
        public virtual User User { get; set; }  // Define a relação com a entidade User correspondente (1:1)

        [ForeignKey("CreatedBy")]
        public virtual User CreatedByUser { get; set; }  // Define a relação com a entidade User para o criador do perfil

        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedByUser { get; set; }  // Define a relação com a entidade User para o atualizador do perfil
    }
}
