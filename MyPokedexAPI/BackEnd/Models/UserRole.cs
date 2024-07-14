using System.ComponentModel.DataAnnotations;  // Importa o namespace para validação de dados
using System.ComponentModel.DataAnnotations.Schema;  // Importa o namespace para mapeamento de dados com o banco de dados

namespace MyPokedexAPI.Models  // Define o namespace para os modelos da aplicação
{
    public class UserRole  // Define a classe UserRole que representa a relação entre um utilizador e um papel no sistema
    {
        [Key]  // Indica que esta propriedade é a chave primária
        public int Id { get; set; }

        [Required]  // Indica que esta propriedade é obrigatória
        public int UserId { get; set; }  // Define a chave estrangeira para o utilizador

        [Required]
        public int RoleId { get; set; }  // Define a chave estrangeira para o papel (role)

        // Propriedades de navegação para relacionamentos com outras entidades
        [ForeignKey("UserId")]
        public virtual User User { get; set; }  // Define a relação com a entidade User

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }  // Define a relação com a entidade Role
    }

    public class UserRoleDTO  // Define a classe UserRoleDTO que representa o Data Transfer Object para UserRole
    {
        [Required]
        public int UserId { get; set; }  // Define a chave estrangeira para o utilizador

        [Required]
        public int RoleId { get; set; }  // Define a chave estrangeira para o papel (role)
    }
}
