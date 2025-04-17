using BlazorRepoEstoque.Models;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IO;

namespace BlazorRepoEstoque.Shared.SharedState
{
    public class ManagerStateAppService
    {

        public EstoqueOrigemEstoqueDestino EstoqueOrigemEstoqueDestino = new();
        public string FarmaciaDestino { get; set; }
        public string periodoImportacaoExcel { get; set; } = string.Empty;

        public List<ReposicaoEstoque> ListImportadaOriginal;

        public List<ReposicaoEstoque> listReposicaoComFiltro;

        public MudNumericField<int?> numericField;
        public MudDateRangePicker _picker;
       
       

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
    }
}
