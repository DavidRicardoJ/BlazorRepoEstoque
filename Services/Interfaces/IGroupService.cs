using BlazorRepoEstoque.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorRepoEstoque.Services.Interfaces
{
    public interface IGroupService
    {
        Task<List<Grupo>> GetNomeGruposAsync();
        Task<Grupo> GetNomeGrupoByIdAsync(int id);
        Task<Grupo> CreateNomeGrupoAsync(Grupo nomeGrupo);
        Task<Grupo> UpdateNomeGrupoAsync(Grupo nomeGrupo);
        Task DeleteNomeGrupoAsync(int id);
        Task AddGrupoProduto(int? nomeGrupoId, int produtoId);
        Task RemoveGrupoProduto(int? nomeGrupoId, int produtoId);
        Task<List<GrupoProduto>> GetGrupoProdutosByGroupId(int nomeGrupoId);
        Task<List<GrupoProduto>> GetAllGrupoProdutos();
    }
}
