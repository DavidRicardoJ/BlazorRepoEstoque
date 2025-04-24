using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace BlazorRepoEstoque.Models
{
    public class LoginUsuarioMV : EstoqueOrigemEstoqueDestino
    {       
        [Required(ErrorMessage = "Campo obrigátorio.")]
        public string UsuarioMV { get; set; }

        [Required(ErrorMessage = "Campo obrigátorio.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public bool IsValidLogin { get; set; }
    }
}
