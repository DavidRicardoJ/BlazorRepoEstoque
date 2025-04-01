using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BlazorRepoEstoque.Models
{
    [Table("tbmedicamento")]
    public class Medicamento
    {
        [Key]
        [Column("idMedicamento")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public int? IdMedicamento { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Column("medicamento")]
        [MaxLength(120)]
        public string NomeMedicamento { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Column("unidade")]
        [MaxLength(45)]
        public string Unidade { get; set; }

        
        public ICollection<GrupoProduto> GruposProdutos { get; set; }
    }
}