using BlazorRepoEstoque.Data.IRepository;
using BlazorRepoEstoque.Models;
using BlazorRepoEstoque.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRepoEstoque.Services
{

    public class GrupoServices : IGrupoServices
    {
        private readonly IGrupoRepository repository;

        public GrupoServices(IGrupoRepository repository)
        {
            this.repository = repository;
        }

        public async Task AddGrupo(Grupo grupo)
        {
            await repository.AddGrupo(grupo);
        }

        public async Task AddAllGrupos(List<Grupo> grupos)
        {
            await repository.AddAllGrupos(grupos);
        }

        public async Task<Grupo> FindByGrupoName(string grupoName)
        {
            var grupo = await repository.FindByGrupoName(grupoName);
            return grupo;
        }

        public async Task<List<Grupo>> GetAllGrupos()
        {
            List<Grupo> grupos = await repository.GetAllGrupos();
            return grupos;
        }

        public async Task<List<string>> GetListaNomeGrupos()
        {
            List<string> GrupoName = new();
            var list = await GetAllGrupos();
            foreach (var item in list)
            {
                GrupoName.Add(item.NomeGrupo);
            }
            return GrupoName.Distinct().OrderBy(x => x).ToList();
        }

        public async Task DeleteGrupo(string nomeGrupo)
        {
            await repository.DeleteGrupo(nomeGrupo);
        }

        public async Task DeleteItemGrupo(List<Grupo> grupoItem)
        {
            await repository.DeleteItemGrupo(grupoItem);
        }

        public Task<List<Grupo>> GetGrupoByName(string nameGroup)
        {
            var result = repository.GetGrupoByName(nameGroup);
            return result;
        }
    }
}
