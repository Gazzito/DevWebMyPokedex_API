using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPokedexAPI.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        // Navegação para o utilizador que criou este role
        [ForeignKey("CreatedBy")]
        public virtual User CreatedByUser { get; set; } = new User();

        // Navegação para o utilizador que atualizou este role
        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedByUser { get; set; } = new User();
    }
}
