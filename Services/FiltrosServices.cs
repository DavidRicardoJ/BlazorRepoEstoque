using BlazorRepoEstoque.Models;
using BlazorRepoEstoque.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;


namespace BlazorRepoEstoque.Services
{
    public class FiltrosServices:IFiltrosServices
    {
        public List<ReposicaoEstoque> FilterByBroup(List<ReposicaoEstoque> listReposição, HashSet<string> SelectedGroups)
        {

            if (SelectedGroups.Contains("Selecionar Grupos")) { SelectedGroups.Remove("Selecionar Grupos"); }

            if (SelectedGroups.Any() is false || listReposição.Any() is false) return new List<ReposicaoEstoque>();

            List<ReposicaoEstoque> listaFiltradaPorGrupo = new();

            foreach (var grupo in SelectedGroups)
            {
                var produto = listReposição.Where(m => m.NomeGrupo == grupo).ToList();
                if (produto != null)
                {
                    listaFiltradaPorGrupo.AddRange(produto);
                }
            }
            return listaFiltradaPorGrupo;
        }

        public List<ReposicaoEstoque> FilterByEspecie(List<ReposicaoEstoque> listReposição, HashSet<string> SelectedEspecies)
        {

            if (SelectedEspecies.Contains("Selecionar Espécies")) { SelectedEspecies.Remove("Selecionar Espécies"); }

            if (SelectedEspecies.Any() is false || listReposição.Any() is false) return new List<ReposicaoEstoque>();

            List<ReposicaoEstoque> listaFiltradaPorEspecie = new();

            foreach (var especie in SelectedEspecies)
            {
                var produto = listReposição.Where(m => m.Especie == especie).ToList();
                if (produto != null)
                {
                    listaFiltradaPorEspecie.AddRange(produto);
                }
            }
            return listaFiltradaPorEspecie;
        }
    }
}
