using BlazorRepoEstoque.Models;
using System.Collections.Generic;

namespace BlazorRepoEstoque.Data
{
    public static class GruposProdutosDto
    {
        public static List<ReposicaoEstoque> ListaProdutosSemGrupoDTO { get; set; } 
        public static List<ReposicaoEstoque> ListaProdutosComGrupoDTO { get; set; }

    }
}
