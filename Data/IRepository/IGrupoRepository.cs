using BlazorRepoEstoque.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorRepoEstoque.Data.IRepository
{
    public interface IGrupoRepository
    {
        Task AddGrupo(Grupo grupo);
        Task AddAllGrupos(List<Grupo> grupos);
        Task<Grupo> FindByGrupoName(string grupoName);       
        Task<List<Grupo>> GetAllGrupos();
        Task<List<Grupo>> GetGrupoByName(string nameGroup);
        Task DeleteGrupo(string nomeGrupo);
        Task DeleteItemGrupo(List<Grupo> grupoItem);
    }
}
