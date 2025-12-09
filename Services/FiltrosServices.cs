using BlazorRepoEstoque.Components;
using BlazorRepoEstoque.Extensions;
using BlazorRepoEstoque.Models;
using BlazorRepoEstoque.Services.Interfaces;
using BlazorRepoEstoque.Shared.SharedState;
using Microsoft.JSInterop;
using MudBlazor;
using System;
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
            if (SelectedEspecies.Contains("MEDICAMENTOS") || SelectedEspecies.Contains("MEDICAMENTOS CONTROLADOS"))
            {
                _managerStateAppService.GrupoEstoque.EstoqueOrigem = 2;
            }
            else
            {
                _managerStateAppService.GrupoEstoque.EstoqueOrigem = 1;
            }
            return listaFiltradaPorEspecie;
        }

        public async Task AplicarFiltrosEConfiguracoes()
        {
            if (_managerStateAppService.ativeProdutoEstoqMin && _managerStateAppService.GrupoEstoque.EstoqueOrigem == null) return;
            if (_managerStateAppService.GetListImportadaOriginal().Any() is false && _managerStateAppService.GetListReposicaoComFiltro().Any() is false) return;
            _managerStateAppService.GetListReposicaoComFiltro().Clear();
            _managerStateAppService.SetListReposicaoComFiltro(CloneListaImportada());

            #region Excluir Itens Duplicados

            if (_managerStateAppService.ExcluirItensDuplicados)
            {
                _managerStateAppService.SetListReposicaoComFiltro(_managerStateAppService.GetListReposicaoComFiltro().GroupBy(g => g.CodigoMV).Select(g => g.First()).ToList());
            }
            #endregion

            #region Filtrar por Espécie

            _managerStateAppService.SetListReposicaoComFiltro(FilterByEspecie(_managerStateAppService.GetListReposicaoComFiltro(), _managerStateAppService.SelectedEspecies));
            #endregion



            #region Filtro por Dias de Estoque

            _managerStateAppService.SetListReposicaoComFiltro (_managerStateAppService.GetListReposicaoComFiltro().Where(d => d.DiasDeEstoque >= 0 && d.DiasDeEstoque <= _managerStateAppService.diasDeEstoque).ToList());
            #endregion

            #region Filtro por Ultimo Movimento
            _managerStateAppService.SetListReposicaoComFiltro (_managerStateAppService.GetListReposicaoComFiltro().Where(u => u.UltimoMovimento >= _managerStateAppService._dateRange.Start && u.UltimoMovimento <= _managerStateAppService._dateRange.End).ToList());
            #endregion


            CalcularReposicao(_managerStateAppService.GetListReposicaoComFiltro());

            #region Filtro por Consumo Zero
            if (_managerStateAppService.ConsumoZero) { _managerStateAppService.SetListReposicaoComFiltro(_managerStateAppService.GetListReposicaoComFiltro().Where(x => x.ConsumoTotal > 0.0f).ToList()); }
            #endregion


            #region Filtro por Reposição Zero
            if (_managerStateAppService.ReposicaoMenorZero) { _managerStateAppService.SetListReposicaoComFiltro(_managerStateAppService.GetListReposicaoComFiltro().Where(x => x.Reposicao >= 0).ToList());}

            #endregion


            _managerStateAppService.SetListReposicaoComFiltro(_managerStateAppService.GetListReposicaoComFiltro().OrderBy(x => x.Medicamento).ToList());

            #region Filtro por Estoque Mínimo
            if (_managerStateAppService.ativeProdutoEstoqMin)
            {
                _managerStateAppService.SetListReposicaoComFiltro(await _produtoEstoqueMinimoService.AddProdutosComEstoqueMin(
                    _managerStateAppService.GetListImportadaOriginal(),
                _managerStateAppService.GetListReposicaoComFiltro(),
                    _managerStateAppService.GrupoEstoque.EstoqueOrigem.Value,
                    _managerStateAppService.GrupoEstoque.EstoqueDestino, _managerStateAppService.diasDeEstoque));

                await MostrarProdutosForaLista();
            }

            #endregion

            #region Filtro por Reposição igual a Zero

            if (_managerStateAppService.ReposicaoIgualZero)
            {
                _managerStateAppService.SetListReposicaoComFiltro(_managerStateAppService.GetListReposicaoComFiltro().Where(r => r.Reposicao != 0).ToList());
            }
            #endregion

            #region Filtro por Grupo

            _managerStateAppService.SetListReposicaoComFiltro( FilterByBroup(_managerStateAppService.GetListReposicaoComFiltro(), _managerStateAppService.SelectedGroups));
            #endregion

            _managerStateAppService.SetListReposicaoComFiltro( ReordenarID(_managerStateAppService.GetListReposicaoComFiltro()));
            if (_managerStateAppService.GetListReposicaoComFiltro().Count() == 0)
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
            IList<ReposicaoEstoque> ListaClonada = _managerStateAppService.GetListImportadaOriginal().Clone();            
            return ListaClonada.ToList();            
        }

        private void CalcularReposicao(List<ReposicaoEstoque> list)
        {
            foreach (var item in list)
            {
                int consumoTotalInt = (int)Math.Ceiling(item.ConsumoTotal);
                item.Reposicao = (int)(consumoTotalInt * _managerStateAppService.fatorReposicao - item.EstoqueAtual);
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
