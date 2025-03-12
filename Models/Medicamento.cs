using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorRepoEstoque.Models
{
    [Table("tbmedicamento")]
    public class Medicamento
    {
        [Key]
        [Column("idMedicamento")]
        public int? IdMedicamento { get; set; }

        [Required]
        [Column("medicamento")]
        [MaxLength(120)]
        public string NomeMedicamento { get; set; }

        [Required]
        [Column("unidade")]
        [MaxLength(45)]
        public string Unidade { get; set; }
    }
}