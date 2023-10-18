using BlazorRepoEstoque.Models;
using System.Collections.Generic;

namespace BlazorRepoEstoque.Data
{
    public static class DataReposicaoDTO
    {
        private static List<ReposicaoEstoque> _ListReposicao { get; set; }
        public static string Farmacia { get; set; }
        private static LoginUsuarioMV Login { get; set; }

        private static IEnumerable<string> Unidades { get; set; }


        public static void SetListReposicao(List<ReposicaoEstoque> ListReposicao, string farmacia)
        {
            _ListReposicao = new List<ReposicaoEstoque>(ListReposicao);
            Farmacia = farmacia;
        }

        public static List<ReposicaoEstoque> GetListReposicao()
        {
            return _ListReposicao;
        }

        public static void InputLoginMV(LoginUsuarioMV login)
        {
            Login = login;
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
