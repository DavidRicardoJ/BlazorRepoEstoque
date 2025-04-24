using BlazorRepoEstoque.Models;
using BlazorRepoEstoque.Shared.SharedState;
using System.Collections.Generic;
using System.Text;


namespace BlazorRepoEstoque.Services
{
    public class CreateScripRobot
    {
        private readonly ManagerStateAppService _managerStateAppService;

        public CreateScripRobot(ManagerStateAppService managerStateAppService)
        {
            _managerStateAppService = managerStateAppService;
        }
        public string GetScriptRobot()
        {
            return ScriptRobot(_managerStateAppService.GetListReposicaoFinal(), _managerStateAppService.loginUsuarioMV, _managerStateAppService.Observacao);
        }

        private string ScriptRobot(List<ReposicaoEstoque> itensReposicaoEstoques, LoginUsuarioMV loginUsuarioMV, string Observação)
        {
            if (itensReposicaoEstoques == null || loginUsuarioMV == null) return null;

            StringBuilder sb = new();
            sb.AppendLine("  ");
            sb.AppendLine($"{loginUsuarioMV.UsuarioMV}");
            sb.AppendLine($"{loginUsuarioMV.Senha}");
            sb.AppendLine($"{loginUsuarioMV.EstoqueOrigem}");
            sb.AppendLine($"{loginUsuarioMV.EstoqueDestino}");
            sb.AppendLine(Observação.ToUpper());

            foreach (var item in itensReposicaoEstoques)
            {
                if (item.Medicamento.Contains('\n') || item.Medicamento.Contains('\r'))
                {
                    item.Medicamento = item.Medicamento.Replace("\n","").Replace("\r","");
                }
                    sb.AppendLine($"{item.CodigoMV}| {item.Medicamento}| {item.Reposicao}");
            }
            return sb.ToString();
        }
    }
}
