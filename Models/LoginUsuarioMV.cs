﻿using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace BlazorRepoEstoque.Models
{
    public class LoginUsuarioMV
    {
        [Required(ErrorMessage = "Campo obrigátorio.")]
        public string UsuarioMV { get; set; }

        [Required(ErrorMessage = "Campo obrigátorio.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required]
        [Min(1, ErrorMessage = "Código não pode ser menor ou igual zero.")]
        public int EstoqueOrigem { get; set; } = 2;

        [Required]
        [Min(1, ErrorMessage = "Código não pode ser menor ou igual zero.")]
        public int EstoqueDestino { get; set; }


    }
}
