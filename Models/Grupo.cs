using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorRepoEstoque.Models
{
    public class Grupo
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Nome do Grupo Obrigatório.")]
        public string NomeGrupo { get; set; }   
        
        [Required(ErrorMessage ="Código do Medicamento Obrigatório.")]            
        public int CodigoMV { get; set; }       
    }
}
