using System.ComponentModel.DataAnnotations;

namespace BlazorRepoEstoque.Models
{
    public class ProdutoEstoqueMinimo
    {
        [Required]
        [Key]
        [Range (1, int.MaxValue, ErrorMessage ="Valor inválido.")]
        public int Id { get; set; }
        [Required(ErrorMessage ="Campo obrigatório.")]
        [StringLength(120,ErrorMessage ="Tamanho máximo 120 caracteres.")]
        public string NomeProduto { get; set; }

        [Range(1, 1000000, ErrorMessage = "O valor deve ser entre 1 e 1.000.000")]
        public int QuantidadeMinima { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Range(1, 1000, ErrorMessage = "O valor deve ser entre 1 e 1000")]
        public int EstoqueOrigem { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Range(1, 1000, ErrorMessage = "O valor deve ser entre 1 e 1000")]
        public int EstoqueSolicitante { get; set; }
    }
}
