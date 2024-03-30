using BlazorRepoEstoque.Models;
using System;
using System.Collections.Generic;

namespace BlazorRepoEstoque.Services
{
    public class DataSharedService
    {
        public event Action? OnChange;
       
        public string Farmacia { get; set; }
        public string Observacao { get; set; }
        private LoginUsuarioMV Login { get; set; }

        private IEnumerable<string> Unidades { get; set; }
        private List<ReposicaoEstoque> _ListReposicao { get; set; }
        private void NotifyStateChanged() => OnChange?.Invoke();


        public void SetListReposicao(List<ReposicaoEstoque> ListReposicao, string farmacia)
        {
            _ListReposicao = ListReposicao;
            Farmacia = farmacia;
            NotifyStateChanged();
        }

        public List<ReposicaoEstoque> GetListReposicao()
        {
            return _ListReposicao;
        }

        public void InputLoginMV(LoginUsuarioMV login)
        {
            Login = login;
            NotifyStateChanged();
        }

        public LoginUsuarioMV GetDadosScript()
        {
            return Login;           
        }

        public void InputUnidades(IEnumerable<string> unidades)
        {
            Unidades = unidades;
            NotifyStateChanged();
        }

        public IEnumerable<string> GetUnidades()
        {
            return Unidades;            
        }

        public void SetObs(string obs)
        {
            Observacao = obs;
        }
        public string GetObservacao()
        {
            return Observacao;
        }
    }
}
