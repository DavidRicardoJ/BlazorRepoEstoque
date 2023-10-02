using System;

namespace BlazorRepoEstoque.Models
{
    public class ReposicaoEstoque
    {
        public int Id { get; set; }
        public string CodigoMV { get; set; }
        public string Medicamento { get; set; }
        public string Unidade { get; set; }
        public DateTime UltimoMovimento { get; set; }
        public float ConsumoTotal { get; set; }
        public float EstoqueAtual { get; set; }
        public int DiasDeEstoque { get; set; }
        public int Reposicao { get; set; }
        public string NomeGrupo { get; set; }
    }
}
