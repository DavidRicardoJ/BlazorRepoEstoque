using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRepoEstoque.Models
{
    public class ScriptAutomation
    {
        [Required(ErrorMessage ="Campo obrigátorio.")]       
        public string UsuarioMV { get; set; }        

        [Required(ErrorMessage = "Campo obrigátorio.")]        
        public string Senha { get; set; }

        [Required]
        [Min(1, ErrorMessage = "Código não pode ser zero.")]
        public int EstoqueOrigem { get; set; } = 2;

        [Required]
        [Min(1, ErrorMessage = "Código não pode ser zero.")]
        public int EstoqueDestino { get; set; }


    }
}
