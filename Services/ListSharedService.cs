using BlazorRepoEstoque.Models;
using System;
using System.Collections.Generic;

namespace BlazorRepoEstoque.Services
{
    public class ListSharedService
    {
        public event Action? OnChange;
        public string Farmacia { get; set; }
        private List<ReposicaoEstoque> _ListReposicao { get; set; }
        public void SetListReposicao(List<ReposicaoEstoque> ListReposicao)
        {
            _ListReposicao = ListReposicao;   
        }

        public List<ReposicaoEstoque> GetListReposicao()
        {
            return _ListReposicao;
        }
    }
}
