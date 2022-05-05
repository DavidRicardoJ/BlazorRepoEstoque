using BlazorRepoEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRepoEstoque.Data
{
    public static class ReportDataReposicao
    {
        private static List<ReposicaoEstoque> _ListReposicao { get; set; }
        public static string Farmacia { get; set; }
        private static LoginUsuarioMV Login { get; set; }

        private static IEnumerable<string> Unidades { get; set; }


        public static void SetListReposicao(List<ReposicaoEstoque> ListReposicao, string farmacia)
        {
            _ListReposicao = ListReposicao;
            Farmacia = farmacia;
        }

        public static List<ReposicaoEstoque> GetListReposicao()
        {
            return _ListReposicao;
        }

        public static void InputDadosScript(LoginUsuarioMV dadosScript)
        {
            Login = dadosScript;
        }

        public static LoginUsuarioMV GetDadosScript()
        {
            return Login;
        }

        public static void InputUnidades(IEnumerable<string> unidades)
        {
            Unidades = unidades;
        }

        public static IEnumerable<string> GetUnidades()
        {
           return Unidades;
        }

    }
}
 