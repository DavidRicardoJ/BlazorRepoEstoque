using BlazorRepoEstoque.Models;
using System.Collections.Generic;

namespace BlazorRepoEstoque.Data
{
    public class ReportDataRequestModel
    {
        public List<ReposicaoEstoque> ListaReposicao { get; set; }
        public string FarmaciaDestino { get; set; }
    }
}
