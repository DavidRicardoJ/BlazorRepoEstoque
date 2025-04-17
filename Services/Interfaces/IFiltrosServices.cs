using BlazorRepoEstoque.Models;
using BlazorRepoEstoque.Shared.SharedState;
using Microsoft.JSInterop;
using MudBlazor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorRepoEstoque.Services.Interfaces
{
    public interface IFiltrosServices
    {
        public List<ReposicaoEstoque> FilterByBroup(List<ReposicaoEstoque> listReposição, IEnumerable<string> SelectedGroups);

        public List<ReposicaoEstoque> FilterByEspecie(List<ReposicaoEstoque> listReposição, IEnumerable<string> SelectedEspecies);

        public List<ReposicaoEstoque> ReordenarID(List<ReposicaoEstoque> list);
        public List<ReposicaoEstoque> CloneListaImportada();
        public Task AplicarFiltrosEConfiguracoes();
      
    }
}
