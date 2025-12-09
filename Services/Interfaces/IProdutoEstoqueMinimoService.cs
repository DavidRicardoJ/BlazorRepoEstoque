
using BlazorRepoEstoque.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorRepoEstoque.Services.Interfaces
{
    public interface IProdutoEstoqueMinimoService
    {
        // Obtém todos os produtos
        Task<List<ProdutoEstoqueMinimo>> GetProdutosEstoqMinAsync();

        // Obtém um produto por ID
        Task<ProdutoEstoqueMinimo> GetProdutoByIdAsync(int id, int estoque);

        // Adiciona um novo produto
        Task<string> AddProdutoAsync(ProdutoEstoqueMinimo produto);

        // Atualiza um produto existente
        Task UpdateProdutoAsync(ProdutoEstoqueMinimo produto);

        // Remove um produto por ID
        Task DeleteProdutoAsync(int id, int estoqueOrigem, int estoqueSolicitante);

        List<ProdutoEstoqueMinimo> GetProdutosForaDasListas();

        Task<List<ReposicaoEstoque>> AddProdutosComEstoqueMin(
            List<ReposicaoEstoque> listaOriginal,
            List<ReposicaoEstoque> listaFiltrada,
            int estoqueOrigem, int estoqueSolicitante,
            int diasDeEstoque);
    }
}
