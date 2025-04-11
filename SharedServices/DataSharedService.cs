using BlazorRepoEstoque.Models;
using System;
using System.Collections.Generic;

namespace BlazorRepoEstoque.SharedServices
{
    public class DataSharedService
    {
        public event Action OnChange;

        private LoginUsuarioMV DadosLogin { get; set; } = new();
        public string Observacao { get; set; }
        public bool IsValidLogin { get; set; }

        private IReadOnlyList<ReposicaoEstoque> _ListReposicao { get; set; } = new List<ReposicaoEstoque>();    


        private void NotifyStateChanged() => OnChange?.Invoke();


        public void SetListReposicao(List<ReposicaoEstoque> ListReposicao, string farmacia)
        {
            _ListReposicao = ListReposicao;
            NotifyStateChanged();
        }

        public List<ReposicaoEstoque> GetListReposicao()
        {
            return (List<ReposicaoEstoque>)_ListReposicao;
        }

        public void SetDadosLoginMV(LoginUsuarioMV login)
        {
            DadosLogin = login;
            NotifyStateChanged();
        }

        public LoginUsuarioMV GetDadosLogin()
        {
            return DadosLogin;
        }
               
    }
}
