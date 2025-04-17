using BlazorRepoEstoque.Components;
using BlazorRepoEstoque.Extensions;
using BlazorRepoEstoque.Models;
using BlazorRepoEstoque.Services.Interfaces;
using BlazorRepoEstoque.Shared.SharedState;
using Microsoft.JSInterop;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlazorRepoEstoque.Services
{
    public class FiltrosServices : IFiltrosServices
    {
        private readonly ManagerStateAppService _managerStateAppService;
        private readonly IProdutoEstoqueMinimoService _produtoEstoqueMinimoService;
        private readonly IJSRuntime _js;
        private readonly IDialogService _dialogService;

        public FiltrosServices(ManagerStateAppService managerStateAppService, IProdutoEstoqueMinimoService produtoEstoqueMinimoService,
            IJSRuntime js, IDialogService dialogService)
        {
            _managerStateAppService = managerStateAppService;
            _produtoEstoqueMinimoService = produtoEstoqueMinimoService;
            _js = js;
            _dialogService = dialogService;
        }
        public List<ReposicaoEstoque> FilterByBroup(List<ReposicaoEstoque> listReposição, IEnumerable<string> SelectedGroups)
        {

            //if (SelectedGroups.Contains("Selecionar Grupos")) { SelectedGroups.Remove("Selecionar Grupos"); }

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

        public List<ReposicaoEstoque> FilterByEspecie(List<ReposicaoEstoque> listReposição, IEnumerable<string> SelectedEspecies)
        {

            //if (SelectedEspecies.Contains("Selecionar Espécies")) { SelectedEspecies.Remove("Selecionar Espécies"); }

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

        public async Task AplicarFiltrosEConfiguracoes()
        {
            if (_managerStateAppService.ativeProdutoEstoqMin && _managerStateAppService.EstoqueOrigemEstoqueDestino.EstoqueOrigem == null) return;
            if (_managerStateAppService.ListImportadaOriginal.Any() is false && _managerStateAppService.listReposicaoComFiltro.Any() is false) return;
            _managerStateAppService.listReposicaoComFiltro.Clear();
            _managerStateAppService.listReposicaoComFiltro = CloneListaImportada();

            #region Excluir Itens Duplicados

            if (_managerStateAppService.ExcluirItensDuplicados)
            {
                _managerStateAppService.listReposicaoComFiltro = _managerStateAppService.listReposicaoComFiltro.GroupBy(g => g.CodigoMV).Select(g => g.First()).ToList();
            }
            #endregion

            #region Filtrar por Espécie

            _managerStateAppService.listReposicaoComFiltro = FilterByEspecie(_managerStateAppService.listReposicaoComFiltro, _managerStateAppService.SelectedEspecies);
            #endregion



            #region Filtro por Dias de Estoque

            _managerStateAppService.listReposicaoComFiltro = _managerStateAppService.listReposicaoComFiltro.Where(d => d.DiasDeEstoque >= 0 && d.DiasDeEstoque <= _managerStateAppService.diasDeEstoque).ToList();
            #endregion

            #region Filtro por Ultimo Movimento
            _managerStateAppService.listReposicaoComFiltro = _managerStateAppService.listReposicaoComFiltro.Where(u => u.UltimoMovimento >= _managerStateAppService._dateRange.Start && u.UltimoMovimento <= _managerStateAppService._dateRange.End).ToList();
            #endregion


            CalcularReposicao(_managerStateAppService.listReposicaoComFiltro);

            #region Filtro por Consumo Zero
            if (_managerStateAppService.ConsumoZero) { _managerStateAppService.listReposicaoComFiltro = _managerStateAppService.listReposicaoComFiltro.Where(x => x.ConsumoTotal > 0.0f).ToList(); }
            #endregion


            #region Filtro por Reposição Zero
            if (_managerStateAppService.ReposicaoMenorZero) { _managerStateAppService.listReposicaoComFiltro = _managerStateAppService.listReposicaoComFiltro.Where(x => x.Reposicao >= 0).ToList(); }

            #endregion


            _managerStateAppService.listReposicaoComFiltro = _managerStateAppService.listReposicaoComFiltro.OrderBy(x => x.Medicamento).ToList();

            #region Filtro por Estoque Mínimo
            if (_managerStateAppService.ativeProdutoEstoqMin)
            {
                _managerStateAppService.listReposicaoComFiltro = await _produtoEstoqueMinimoService.AddProdutosComEstoqueMin(
                    _managerStateAppService.ListImportadaOriginal,
                _managerStateAppService.listReposicaoComFiltro,
                    _managerStateAppService.EstoqueOrigemEstoqueDestino.EstoqueOrigem.Value,
                    _managerStateAppService.EstoqueOrigemEstoqueDestino.EstoqueDestino);
                await MostrarProdutosForaLista();
            }

            #endregion

            #region Filtro por Reposição igual a Zero

            if (_managerStateAppService.ReposicaoIgualZero)
            {
                _managerStateAppService.listReposicaoComFiltro = _managerStateAppService.listReposicaoComFiltro.Where(r => r.Reposicao != 0).ToList();
            }
            #endregion

            #region Filtro por Grupo

            _managerStateAppService.listReposicaoComFiltro = FilterByBroup(_managerStateAppService.listReposicaoComFiltro, _managerStateAppService.SelectedGroups);
            #endregion

            _managerStateAppService.listReposicaoComFiltro = ReordenarID(_managerStateAppService.listReposicaoComFiltro);
            if (_managerStateAppService.listReposicaoComFiltro.Count() == 0)
            {
                _managerStateAppService.alert = true;
                _managerStateAppService.filterItens = false;
            }
            else
            {
                _managerStateAppService.alert = false;
                _managerStateAppService.filterItens = true;
                ClosePanel();
                AddEstiloPosFiltro();
            }           
        }

        public List<ReposicaoEstoque> CloneListaImportada()
        {
            IList<ReposicaoEstoque> ListaClonada = _managerStateAppService.ListImportadaOriginal.Clone();
            return ListaClonada.ToList();
        }

        private void CalcularReposicao(List<ReposicaoEstoque>list)
        {
            foreach (var item in list)
            {
                item.Reposicao = ((int)item.ConsumoTotal * _managerStateAppService.fatorReposicao - (int)item.EstoqueAtual);
            }
        }

        private Task MostrarProdutosForaLista()
        {
            var listaProdutosForaLista = _produtoEstoqueMinimoService.GetProdutosForaDasListas();

            if (listaProdutosForaLista != null && listaProdutosForaLista.Any())
            {
                List
                <string>
                    DialogContent = new();
                for (int i = 0; i < listaProdutosForaLista.Count; i++)
                {
                    DialogContent.Add("Cód. " + listaProdutosForaLista[i].Id + " - " + listaProdutosForaLista[i].NomeProduto);
                }


                string Title = "Estes produtos com Estoque Mínimo não consta na Importação do Excel, adicione-os manualmente.";
                var options = new DialogOptions { CloseButton = false };
                var parameters = new DialogParameters<ComponetMultLineiDialog>
                            {
                            { x => x.MultiLineMessage, DialogContent},
                            };
                return _dialogService.ShowAsync<ComponetMultLineiDialog>(Title, parameters, options);
            }
            return Task.CompletedTask;
        }

        public List<ReposicaoEstoque> ReordenarID(List<ReposicaoEstoque> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Id = i;
            }
            return list;
        }

        private void ClosePanel()
        {
            _js.InvokeVoidAsync("ClickElementByClass", "mud-expand-panel-header");
        }

        private void AddEstiloPosFiltro()
        {
            _js.InvokeVoidAsync("AdicionarEstiloPosFiltro");
        }
    }
}
