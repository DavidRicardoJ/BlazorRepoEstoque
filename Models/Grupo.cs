using System.Collections.Generic;

namespace BlazorRepoEstoque.Models
{
    public class Grupo
    {
        public int? Id { get; set; }
        public string GroupName { get; set; }

        public ICollection<GrupoProduto> GruposProdutos { get; set; }
    }
}
