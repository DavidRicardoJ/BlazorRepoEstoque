using BlazorRepoEstoque.Data;
using BlazorRepoEstoque.Models;
using System.Collections.Generic;
using System.Text;

namespace BlazorRepoEstoque.Services
{
    public class CreateScripRobot
    {
        public string GetScriptRobot()
        {
            return ScriptRobot(DataReposicaoDTO.GetListReposicao(), DataReposicaoDTO.GetDadosScript());
        }

        private string ScriptRobot(List<ReposicaoEstoque> itensReposicaoEstoques, LoginUsuarioMV loginUsuarioMV)
        {
            if (itensReposicaoEstoques == null || loginUsuarioMV == null) return null;

            StringBuilder sb = new();
            sb.AppendLine("  ");
            sb.AppendLine($"{loginUsuarioMV.UsuarioMV}");
            sb.AppendLine($"{loginUsuarioMV.Senha}");
            sb.AppendLine($"{loginUsuarioMV.EstoqueOrigem}");
            sb.AppendLine($"{loginUsuarioMV.EstoqueDestino}");
            sb.AppendLine($"Reposição de Estoque >> {DataReposicaoDTO.Farmacia}");

            foreach (var item in itensReposicaoEstoques)
            {
                sb.AppendLine($"{item.CodigoMV}| {item.Medicamento}| {item.Reposicao}");
            }
            return sb.ToString();
        }
    }
}
