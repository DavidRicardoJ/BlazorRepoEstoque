using BlazorRepoEstoque.Models;
using BlazorRepoEstoque.Services;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IO;

namespace BlazorRepoEstoque.Shared.SharedState
{
    public class ManagerStateAppService
    {
        public event Action OnChange;

        public EstoqueOrigemEstoqueDestino GrupoEstoque = new();

        public string periodoImportacaoExcel { get; set; } = string.Empty;


        #region Listas de Reposição        
        private List<ReposicaoEstoque> ListImportadaOriginal { get; set; } = new();
        public List<ReposicaoEstoque> GetListImportadaOriginal()
        {
            return ListImportadaOriginal;
        }
        public void SetListImportadaOriginal(List<ReposicaoEstoque> listImportadaOriginal)
        {
            ListImportadaOriginal = listImportadaOriginal;
            NotifyChanged();
        }


        private List<ReposicaoEstoque> ListReposicaoComFiltro { get; set; } = new();

        public List<ReposicaoEstoque> GetListReposicaoComFiltro()
        {
            return ListReposicaoComFiltro;
        }
        public void SetListReposicaoComFiltro(List<ReposicaoEstoque> listReposicaoComFiltro)
        {
            ListReposicaoComFiltro = listReposicaoComFiltro;
            NotifyChanged();
        }

        private List<ReposicaoEstoque> ListReposicaoFinal { get; set; } = new();

        public List<ReposicaoEstoque> GetListReposicaoFinal()
        {
            return ListReposicaoFinal;
        }
        public void SetListReposicaoFinal(List<ReposicaoEstoque> listReposicaoFinal)
        {
            ListReposicaoFinal = listReposicaoFinal;
        }

        #endregion


        #region Controle de exibição do componente

        public MudNumericField<int?> numericField;

        public MudDateRangePicker _picker;

        #endregion


        #region MessageBox e Avisos

        public bool alert = false;
        public bool erroImport = false;
        public bool showMessageBox;
        public string tituloMsgBox;
        public string avisoMsgBox;
        public bool progress = false;
        public bool numericFieldErro;
        #endregion


        public IEnumerable<string> Unidades;

        //public DialogOptions disableBackdropClick = new DialogOptions() { BackdropClick = true };
        public bool GerarRelatorio;

        #region Configurações de filtragem

        public bool ConsumoZero { get; set; } = true;
        public bool ReposicaoMenorZero { get; set; } = true;
        public bool ReposicaoIgualZero { get; set; } = false;
        public bool ExcluirItensDuplicados { get; set; } = true;

        public bool ativeProdutoEstoqMin = false;

        public int fatorReposicao = 1;

        public bool filterItens = false; // itens já foram filtrados?

        public int diasDeEstoque = 5;

        public DateRange _dateRange = new DateRange(DateTime.Now.AddDays(-4).Date, DateTime.Now.Date);

        public IEnumerable<string> SelectedEspecies = new HashSet<string>();
        public IEnumerable<string> SelectedGroups { get; set; }
        #endregion

        public MemoryStream stream2;




        //Gerenciamento do estado do componente TabPage
        #region Configurações da tabela

        public bool dense { get; set; } = true;
        public bool hover { get; set; } = true;
        public bool striped { get; set; } = true;
        public bool bordered { get; set; } = false;
        public string searchString1 { get; set; } = "";
        public string searchString2 { get; set; } = "";
        public int[] pagesize { get; set; } = { 10, 15, 30, 100, 1000 };
        public int RowsPerPage { get; set; }
        public ReposicaoEstoque selectedItem1 { get; set; } = null;

        // public HashSet<ReposicaoEstoque> selectedItems = new HashSet<ReposicaoEstoque>();
        #endregion

        //Gerenciamento do estado do componente Login
        public string Observacao { get; set; } = string.Empty;

        public LoginUsuarioMV loginUsuarioMV = new();

        public void NotifyChanged()
        {
            OnChange?.Invoke();
        }

        //Gerencia a criação do script robot

        public string GetScriptRobot()
        {
            CreateScripRobot createScripRobot = new(this);
            return createScripRobot.GetScriptRobot();
        }

    }
}
