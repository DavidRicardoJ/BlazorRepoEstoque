using BlazorRepoEstoque.Extensions;
using BlazorRepoEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorRepoEstoque.Services
{
    public class ListSharedService
    {
        public event Action? OnChange;
        public string Farmacia { get; set; }
        public int CodigoEstoque { get; set; }
        private IList<ReposicaoEstoque> _ListReposicao { get; set; }
        public void SetListReposicao(List<ReposicaoEstoque> ListReposicao)
        {
            _ListReposicao = ListReposicao.Clone();   
        }

        public List<ReposicaoEstoque> GetListReposicao()
        {
            return _ListReposicao.ToList();
        }
    }
}
