using BlazorRepoEstoque.Models;
using System.Collections.Generic;

namespace BlazorRepoEstoque.Services.Interfaces
{
    public interface IFiltrosServices
    {
        public List<ReposicaoEstoque> FilterByBroup(List<ReposicaoEstoque> listReposição, IEnumerable<string> SelectedGroups);

        public List<ReposicaoEstoque> FilterByEspecie(List<ReposicaoEstoque> listReposição, IEnumerable<string> SelectedEspecies);
    }
}
