using System;  // Importa o namespace para funcionalidades básicas do sistema
using System.ComponentModel.DataAnnotations;  // Importa o namespace para validação de dados
using System.ComponentModel.DataAnnotations.Schema;  // Importa o namespace para mapeamento de dados com o banco de dados

namespace MyPokedexAPI.Models  // Define o namespace para os modelos da aplicação
{
    public class TotalPacksOpenedRanking  // Define a classe TotalPacksOpenedRanking que representa o ranking de pacotes abertos
    {
        [Key]  // Indica que esta propriedade é a chave primária
        [ForeignKey("User")]  // Indica que esta propriedade é uma chave estrangeira para a entidade User
        public int Id { get; set; }

        [Required]  // Indica que esta propriedade é obrigatória
        public int TotalPacksOpened { get; set; }  // Define o total de pacotes abertos

        [Required]  // Indica que esta propriedade é obrigatória
        public int Rank { get; set; }  // Define o rank do utilizador baseado no total de pacotes abertos

        [Required]  // Indica que esta propriedade é obrigatória
        public DateTime CreatedOn { get; set; }  // Define a data de criação do ranking

        [Required]  // Indica que esta propriedade é obrigatória
        public int CreatedBy { get; set; }  // Define o ID do utilizador que criou o ranking

        public DateTime? UpdatedOn { get; set; }  // Define a data de atualização do ranking

        public int? UpdatedBy { get; set; }  // Define o ID do utilizador que atualizou o ranking

        // Navegação para o utilizador que criou este ranking
        [ForeignKey("CreatedBy")]
        public virtual User CreatedByUser { get; set; }  // Define a relação com a entidade User para o criador do ranking

        // Navegação para o utilizador que atualizou este ranking
        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedByUser { get; set; }  // Define a relação com a entidade User para o atualizador do ranking

        // Navegação para o utilizador proprietário deste ranking
        public virtual User User { get; set; }  // Define a relação com a entidade User para o utilizador associado a este ranking
    }
}
