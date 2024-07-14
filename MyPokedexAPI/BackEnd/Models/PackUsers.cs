using System;  // Importa o namespace para funcionalidades básicas do sistema
using System.ComponentModel.DataAnnotations;  // Importa o namespace para validação de dados
using System.ComponentModel.DataAnnotations.Schema;  // Importa o namespace para mapeamento de dados com o banco de dados

namespace MyPokedexAPI.Models  // Define o namespace para os modelos da aplicação
{
    public class PackUsers  // Define a classe PackUsers que representa a relação entre um utilizador e um pack
    {
        [Key]  // Indica que esta propriedade é a chave primária
        public int Id { get; set; }

        [Required]  // Indica que esta propriedade é obrigatória
        public int UserId { get; set; }  // Define a chave estrangeira para o utilizador

        [Required]  // Indica que esta propriedade é obrigatória
        public int PackId { get; set; }  // Define a chave estrangeira para o pack

        [Required]  // Indica que esta propriedade é obrigatória
        public DateTime OpenedOn { get; set; }  // Define a data e hora em que o pack foi aberto

        // Navegação para o utilizador associado
        [ForeignKey("UserId")]
        public virtual User User { get; set; }  // Define a relação com a entidade User

        // Navegação para o pack associado
        [ForeignKey("PackId")]
        public virtual Pack Pack { get; set; }  // Define a relação com a entidade Pack
    }
}
