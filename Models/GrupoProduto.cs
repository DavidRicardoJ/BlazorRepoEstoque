namespace BlazorRepoEstoque.Models
{
    public class GrupoProduto
    {
        public int NomeGrupoID { get; set; }
        public Grupo Grupo { get; set; }
        public int ProdutoID { get; set; }
        public Medicamento Produto { get; set; }
    }
}
