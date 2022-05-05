using BlazorRepoEstoque.Data.IRepository;
using BlazorRepoEstoque.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorRepoEstoque.Services.Interfaces
{
    public interface IGrupoServices
    {
        Task AddGrupo(Grupo grupo);
        Task AddAllGrupos(List<Grupo> grupos);
        Task<List<Grupo>> GetAllGrupos();
        Task<Grupo> FindByGrupoName(string grupoName);
        Task<List<string>> GetListaNomeGrupos();
        Task DeleteGrupo(string nomeGrupo);
        Task DeleteItemGrupo(List<Grupo> grupoItem);
        Task<List<Grupo>> GetGrupoByName(string nameGroup);
    }
}
