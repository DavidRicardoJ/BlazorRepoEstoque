using BlazorRepoEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRepoEstoque.Data
{
    public static class ReportDataReposicao
    {
        public static List<ReposicaoEstoque> _ListReposicao { get; set; }
        public static string Farmacia { get; set; }


        public static void SetListReposicao(List<ReposicaoEstoque> ListReposicao, string farmacia)
        {
           _ListReposicao = ListReposicao;
            Farmacia = farmacia;
        }

        public static List<ReposicaoEstoque> GetListReposicao()
        {
            return _ListReposicao;
        }
    }
}
