using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace BlazorRepoEstoque.Models
{
    public class EstoqueOrigemEstoqueDestino
    {
        [Required]
        [Min(1, ErrorMessage = "Código não pode ser menor ou igual zero.")]
        public int? EstoqueOrigem { get; set; }

        [Required]
        [Min(1, ErrorMessage = "Código não pode ser menor ou igual zero.")]
        public int EstoqueDestino { get; set; }

        public string FarmaciaDestino { get; set; }
    }
}
