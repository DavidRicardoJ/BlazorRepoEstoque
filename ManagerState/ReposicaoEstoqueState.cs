using BlazorRepoEstoque.Extensions;
using BlazorRepoEstoque.Models;
using BlazorRepoEstoque.Services;
using BlazorRepoEstoque.Services.Interfaces;
using BlazorRepoEstoque.SharedServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BlazorRepoEstoque.ManagerState
{
    public class ReposicaoEstoqueState
    {
        // Eventos
        public event Action OnChange;
        public event Action<bool> OnProgressChange;
        public event Action<string, string> OnShowMessage;
        public event Action<bool> OnAlertVisibilityChange;
        public event Action<bool> OnErroImportChange;
        public event Action<bool> OnFilterItensChange;
        public event Action<bool> OnGerarRelatorioChange;

        // Dependências injetadas
        private readonly IGroupService _grupoService;
        private readonly IFiltrosServices _filtrosServices;
        private readonly IProdutoEstoqueMinimoService _produtoEstoqueMinimoService;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        private readonly ImportExcelService _excelService;
        private readonly DataSharedService _dataSharedService;

        // Estado
        private bool _progress;
        private bool _alert;
        private bool _erroImport;
        private bool _filterItens;
        private bool _gerarRelatorio;
        private bool _showMessageBox;
        private string _tituloMsgBox;
        private string _avisoMsgBox;

        // Dados
        public List<ReposicaoEstoque> ListImportadaOriginal { get; private set; }
        public List<ReposicaoEstoque> ListReposicaoComFiltro { get; private set; }
        public DateRange DateRange { get; private set; }
        public double DiasDeEstoque { get; private set; }
        public bool ConsumoZero { get; private set; }
        public bool ReposicaoMenorZero { get; private set; }
        public bool ReposicaoIgualZero { get; private set; }
        public bool ExcluirItensDuplicados { get; private set; }
        public int FatorReposicao { get; private set; }
        public bool AtivarProdutoEstoqueMin { get; private set; }
        public int? CodigoEstoqueOrigem { get; private set; }
        public IEnumerable<string> SelectedEspecies { get; private set; }
        public IEnumerable<string> SelectedGroups { get; private set; }
        public string Farmacia { get; private set; }
        public int EstoqueDestino { get; private set; }
        public string PeriodoImportacaoExcel { get; private set; }

        public ReposicaoEstoqueState(
            IGroupService grupoService,
            IFiltrosServices filtrosServices,
            IProdutoEstoqueMinimoService produtoEstoqueMinimoService,
            IJSRuntime jsRuntime,
            NavigationManager navigationManager,
            ImportExcelService excelService,
             DataSharedService dataSharedService)
        {
            _grupoService = grupoService;
            _filtrosServices = filtrosServices;
            _produtoEstoqueMinimoService = produtoEstoqueMinimoService;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _excelService = excelService;
            _dataSharedService = dataSharedService;
            // Inicialização dos dados
            Initialize();
        }

        private void Initialize()
        {
            ListImportadaOriginal = new List<ReposicaoEstoque>();
            ListReposicaoComFiltro = new List<ReposicaoEstoque>();
            DateRange = new DateRange(DateTime.Now.AddDays(-4).Date, DateTime.Now.Date);
            DiasDeEstoque = 5;
            ConsumoZero = true;
            ReposicaoMenorZero = true;
            ReposicaoIgualZero = false;
            ExcluirItensDuplicados = true;
            FatorReposicao = 1;
            SelectedEspecies = new HashSet<string>();
            SelectedGroups = new HashSet<string>();
        }

        // Métodos para manipulação de estado
        public void SetProgress(bool value)
        {
            _progress = value;
            OnProgressChange?.Invoke(_progress);
        }

        public void SetAlert(bool value)
        {
            _alert = value;
            OnAlertVisibilityChange?.Invoke(_alert);
        }

        public void SetErroImport(bool value)
        {
            _erroImport = value;
            OnErroImportChange?.Invoke(_erroImport);
        }

        public void SetFilterItens(bool value)
        {
            _filterItens = value;
            OnFilterItensChange?.Invoke(_filterItens);
        }

        public void SetGerarRelatorio(bool value)
        {
            _gerarRelatorio = value;
            OnGerarRelatorioChange?.Invoke(_gerarRelatorio);
        }

        public void ShowMessage(string titulo, string aviso)
        {
            _tituloMsgBox = titulo;
            _avisoMsgBox = aviso;
            _showMessageBox = true;
            OnShowMessage?.Invoke(_tituloMsgBox, _avisoMsgBox);
        }

        // Manipulação de dados
        public void SetDateRange(DateRange dateRange)
        {
            DateRange = dateRange;
            NotifyStateChanged();
        }

        public void SetDiasDeEstoque(double value)
        {
            DiasDeEstoque = value;
            NotifyStateChanged();
        }

        public void SetConsumoZero(bool value)
        {
            ConsumoZero = value;
            NotifyStateChanged();
        }

        public void SetReposicaoMenorZero(bool value)
        {
            ReposicaoMenorZero = value;
            NotifyStateChanged();
        }

        public void SetReposicaoIgualZero(bool value)
        {
            ReposicaoIgualZero = value;
            NotifyStateChanged();
        }

        public void SetExcluirItensDuplicados(bool value)
        {
            ExcluirItensDuplicados = value;
            NotifyStateChanged();
        }

        public void SetFatorReposicao(int value)
        {
            FatorReposicao = value;
            NotifyStateChanged();
        }

        public void SetAtivarProdutoEstoqueMin(bool value)
        {
            AtivarProdutoEstoqueMin = value;
            NotifyStateChanged();
        }

        public void SetCodigoEstoqueOrigem(int? value)
        {
            CodigoEstoqueOrigem = value;
            NotifyStateChanged();
        }

        public void SetSelectedEspecies(IEnumerable<string> especies)
        {
            SelectedEspecies = especies;
            NotifyStateChanged();
        }

        public void SetSelectedGroups(IEnumerable<string> groups)
        {
            SelectedGroups = groups;
            NotifyStateChanged();
        }

        // Métodos de negócio
        public async Task GetFile(InputFileChangeEventArgs e)
        {
            SetProgress(true);
            try
            {
                // Preparar o stream do arquivo
                var stream1 = e.File.OpenReadStream();
                var stream2 = new MemoryStream();
                await stream1.CopyToAsync(stream2);
                stream1.Close();

                // Importar Excel
                ListImportadaOriginal = await _excelService.ReadExcel(stream2);
                if (ListImportadaOriginal == null)
                {
                    SetErroImport(true);
                    SetProgress(false);
                    return;
                }

                SetErroImport(false);

                // Processar dados importados
                ListImportadaOriginal = ReordenarID(ListImportadaOriginal);
                ListImportadaOriginal = ListImportadaOriginal.OrderBy(x => x.Medicamento).ToList();
                ListImportadaOriginal = await AddGrupo(ListImportadaOriginal);

                ListReposicaoComFiltro = CloneListaImportada();

                // Carregar filtros iniciais
                LoadGrupos();
                LoadEspecies();

                // Guardar informações da farmácia
                if (ListImportadaOriginal.Any())
                {
                    Farmacia = ListImportadaOriginal[0].Farmacia;
                    EstoqueDestino = ListImportadaOriginal[0].CodigoEstoque;
                    PeriodoImportacaoExcel = ListImportadaOriginal[0].PeriodoImportacaoExcel;
                }

                NotifyStateChanged();
            }
            finally
            {
                SetProgress(false);
            }
        }

        private List<ReposicaoEstoque> CloneListaImportada()
        {
            IList<ReposicaoEstoque> listaClonada = ListImportadaOriginal.Clone();
            return listaClonada.ToList();
        }

        private void LoadGrupos()
        {
            SelectedGroups = GetGrupos().ToHashSet();
        }

        private void LoadEspecies()
        {
            SelectedEspecies = GetEspecies().ToHashSet();
        }

        public IEnumerable<string> GetEspecies()
        {
            return ListImportadaOriginal.OrderBy(x => x.Especie).Select(x => x.Especie).Distinct().ToList();
        }

        public IEnumerable<string> GetGrupos()
        {
            return ListImportadaOriginal.OrderBy(x => x.NomeGrupo).Select(x => x.NomeGrupo).Distinct().ToList();
        }

        public IEnumerable<string> GetUnidades()
        {
            return ListReposicaoComFiltro.OrderBy(x => x.Unidade).Select(x => x.Unidade).Distinct().ToList();
        }

        private List<ReposicaoEstoque> ReordenarID(List<ReposicaoEstoque> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Id = i;
            }
            return list;
        }

        private async Task<List<ReposicaoEstoque>> AddGrupo(List<ReposicaoEstoque> list)
        {
            var grupos = await _grupoService.GetAllGrupoProdutos();

            if (grupos is not null)
            {
                foreach (var item in list)
                {
                    var nomeGrupo = grupos.FirstOrDefault(x => x.Produto.IdMedicamento.ToString() == item.CodigoMV);

                    if (nomeGrupo is not null)
                    {
                        item.NomeGrupo = nomeGrupo.Grupo.GroupName;
                    }
                    else
                    {
                        item.NomeGrupo = "PRODUTOS SEM GRUPO";
                    }
                }
            }

            return list;
        }

        private void CalcularReposicao(List<ReposicaoEstoque> list)
        {
            foreach (var item in list)
            {
                item.Reposicao = ((int)item.ConsumoTotal * FatorReposicao - (int)item.EstoqueAtual);
            }
        }

        public async Task AplicarFiltrosEConfiguracoes()
        {
            if (AtivarProdutoEstoqueMin && CodigoEstoqueOrigem == null) return;
            if (!ListImportadaOriginal.Any() && !ListReposicaoComFiltro.Any()) return;

            SetProgress(true);

            try
            {
                ListReposicaoComFiltro.Clear();
                ListReposicaoComFiltro = CloneListaImportada();

                // Excluir Itens Duplicados
                if (ExcluirItensDuplicados)
                {
                    ListReposicaoComFiltro = ListReposicaoComFiltro.GroupBy(g => g.CodigoMV).Select(g => g.First()).ToList();
                }

                // Filtrar por Espécie
                ListReposicaoComFiltro = _filtrosServices.FilterByEspecie(ListReposicaoComFiltro, SelectedEspecies);

                // Filtro por Grupo
                ListReposicaoComFiltro = _filtrosServices.FilterByBroup(ListReposicaoComFiltro, SelectedGroups);

                // Filtro por Dias de Estoque
                ListReposicaoComFiltro = ListReposicaoComFiltro.Where(d => d.DiasDeEstoque >= 0 && d.DiasDeEstoque <= DiasDeEstoque).ToList();

                // Filtro por Último Movimento
                ListReposicaoComFiltro = ListReposicaoComFiltro.Where(u => u.UltimoMovimento >= DateRange.Start && u.UltimoMovimento <= DateRange.End).ToList();

                // Calcular Reposição
                CalcularReposicao(ListReposicaoComFiltro);

                // Filtro por Consumo Zero
                if (ConsumoZero)
                {
                    ListReposicaoComFiltro = ListReposicaoComFiltro.Where(x => x.ConsumoTotal > 0.0f).ToList();
                }

                // Filtro por Reposição Menor que Zero
                if (ReposicaoMenorZero)
                {
                    ListReposicaoComFiltro = ListReposicaoComFiltro.Where(x => x.Reposicao >= 0).ToList();
                }

                ListReposicaoComFiltro = ListReposicaoComFiltro.OrderBy(x => x.Medicamento).ToList();

                // Filtro por Estoque Mínimo
                if (AtivarProdutoEstoqueMin)
                {
                    ListReposicaoComFiltro = await _produtoEstoqueMinimoService.AddProdutosComEstoqueMin(
                        ListImportadaOriginal,
                        ListReposicaoComFiltro,
                        CodigoEstoqueOrigem.Value,
                        EstoqueDestino);

                    await MostrarProdutosForaLista();
                }

                // Filtro por Reposição igual a Zero
                if (ReposicaoIgualZero)
                {
                    ListReposicaoComFiltro = ListReposicaoComFiltro.Where(r => r.Reposicao != 0).ToList();
                }

                ListReposicaoComFiltro = ReordenarID(ListReposicaoComFiltro);

                if (!ListReposicaoComFiltro.Any())
                {
                    SetAlert(true);
                    SetFilterItens(false);
                }
                else
                {
                    SetAlert(false);
                    SetFilterItens(true);
                    await ClosePanel();
                    await AddEstiloPosFiltro();

                }
            }
            finally
            {
                SetProgress(false);
            }
        }

        public async Task ClosePanel()
        {
            await _jsRuntime.InvokeVoidAsync("ClickElementByClass", "mud-expand-panel-header");
        }

        public async Task AddEstiloPosFiltro()
        {
            await _jsRuntime.InvokeVoidAsync("AdicionarEstiloPosFiltro");
        }

        public bool VerificaItemZeradoOuNegativo()
        {
            var itensZerados = ListReposicaoComFiltro.Where(x => x.Reposicao <= 0).ToList();
            if (itensZerados.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Há itens zerados ou negativos na lista de reposição: ");
                foreach (var item in itensZerados)
                {
                    sb.AppendLine("Cód." + item.CodigoMV + " " + item.Medicamento);
                }
                var aviso = sb.ToString();
                ShowMessage("Aviso", aviso);
                return true;
            }
            else
            {
                SetAlert(false);
                return false;
            }
        }

        public bool VerificaItensDuplicados()
        {
            var itensDuplicados = ListReposicaoComFiltro.GroupBy(g => g.CodigoMV).Where(c => c.Count() > 1).Select(s => s.Key).ToList();

            if (itensDuplicados.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Há itens duplicados na lista de reposição: ");
                foreach (var item in itensDuplicados)
                {
                    var itemDuplicado = ListReposicaoComFiltro.Where(x => x.CodigoMV.Equals(item)).FirstOrDefault();
                    if (itemDuplicado is not null)
                    {
                        sb.AppendLine("Cód." + itemDuplicado.CodigoMV + " " + itemDuplicado.Medicamento);
                    }
                }
                var aviso = sb.ToString();
                ShowMessage("Aviso", aviso);
                return true;
            }
            else
            {
                SetAlert(false);
                return false;
            }
        }

        public void GeraRelatorio()
        {
            if (ListReposicaoComFiltro.Count == 0 || !_filterItens) return;
            if (VerificaItemZeradoOuNegativo()) return;

            SetGerarRelatorio(true);
        }

        public void CriarScriptAutomacao()
        {
            if (VerificaItemZeradoOuNegativo()) return;
            if (VerificaItensDuplicados()) return;
            if (ListReposicaoComFiltro.Count == 0 || !_filterItens) return;

            LoginUsuarioMV loginUsuarioMV = new()
            {
                FarmaciaDestino = Farmacia,
                EstoqueDestino = EstoqueDestino,
                EstoqueOrigem = CodigoEstoqueOrigem ?? 3,
            };

            _dataSharedService.SetDadosLoginMV(loginUsuarioMV);

            _navigationManager.NavigateTo("/robot", true);
        }

        //public async Task NavigateToNewTab()
        //{
        //    string url = "/Robot";
        //    await _jsRuntime.InvokeAsync<object>("open", url, "_blank");
        //}

        public async Task MostrarProdutosForaLista()
        {
            var listaProdutosForaLista = _produtoEstoqueMinimoService.GetProdutosForaDasListas();

            if (listaProdutosForaLista != null && listaProdutosForaLista.Any())
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Estes produtos com Estoque Mínimo não constam na Importação do Excel, adicione-os manualmente:");

                foreach (var produto in listaProdutosForaLista)
                {
                    sb.AppendLine($"Cód. {produto.Id} - {produto.NomeProduto}");
                }

                ShowMessage("Produtos não encontrados", sb.ToString());
            }
        }

        public async Task AddNovoProduto(BlazorRepoEstoque.Components.ComponentAddProduto.ProductModel model)
        {
            var item = ListReposicaoComFiltro.FirstOrDefault(x => x.CodigoMV == model.IdMedicamento.ToString());
            if (item != null)
            {
                ShowMessage("Atenção", "O produto já se encontra na lista!");
                return;
            }

            var grupos = await _grupoService.GetAllGrupoProdutos();
            string nomeGrupo = "PRODUTOS SEM GRUPO";

            var grupoProduto = grupos.FirstOrDefault(x => x.Produto.IdMedicamento.ToString() == model.IdMedicamento.ToString());
            if (grupoProduto?.Grupo != null)
            {
                nomeGrupo = grupoProduto.Grupo.GroupName;
            }

            ListReposicaoComFiltro.Add(new ReposicaoEstoque
            {
                CodigoMV = model.IdMedicamento.ToString(),
                Reposicao = (int)model.Quantidade,
                Medicamento = model.NomeMedicamento,
                UltimoMovimento = DateTime.Now,
                Unidade = model.Unidade,
                IsAdd = true,
                NomeGrupo = nomeGrupo
            });

            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}

