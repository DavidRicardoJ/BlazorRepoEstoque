using BlazorRepoEstoque.Data;
using BlazorRepoEstoque.Models;
using BlazorRepoEstoque.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRepoEstoque.Services
{
    public class ProdutoEstoqueMinimoService : IProdutoEstoqueMinimoService
    {
        private readonly AppDbContext _context;
        private List<ProdutoEstoqueMinimo> ProdutosForaDasLista = new();

        public ProdutoEstoqueMinimoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProdutoEstoqueMinimo>> GetProdutosEstoqMinAsync()
        {
            return await _context.ProdutoEstoqueMinimo.ToListAsync();
        }

        public async Task<ProdutoEstoqueMinimo> GetProdutoByIdAsync(int id, int estoque)
        {
            return await _context.ProdutoEstoqueMinimo.Where(p => p.Id == id && p.EstoqueSolicitante == estoque).FirstOrDefaultAsync();
        }

        public async Task<string> AddProdutoAsync(ProdutoEstoqueMinimo produto)
        {
            try
            {
                _context.ProdutoEstoqueMinimo.Add(produto);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("cannot be tracked because another instance with the same key value"))
                {
                    return "Produto já cadastrado";
                }
                return e.Message;
            }
            return null;
        }

        public async Task UpdateProdutoAsync(ProdutoEstoqueMinimo produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProdutoAsync(int id, int estoqueSolicitante, int estoqueOrigem)
        {
            var produto = await _context.ProdutoEstoqueMinimo.Where(p => p.Id == id
            && p.EstoqueOrigem == estoqueOrigem
            && p.EstoqueSolicitante == estoqueSolicitante
            ).FirstOrDefaultAsync();
            if (produto != null)
            {
                _context.ProdutoEstoqueMinimo.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }

        public List<ProdutoEstoqueMinimo> GetProdutosForaDasListas()
        {
            return ProdutosForaDasLista;
        }

        public async Task<List<ReposicaoEstoque>> AddProdutosComEstoqueMin(
            List<ReposicaoEstoque> listaOriginal,
            List<ReposicaoEstoque> listaFiltrada,
            int estoqueOrigem, int estoqueSolicitante,
            int diasDeEstoque)
        {
            ProdutosForaDasLista.Clear();
            var produtosEstoqueMinimo = await _context.ProdutoEstoqueMinimo.
                Where(p => p.EstoqueOrigem == estoqueOrigem && p.EstoqueSolicitante == estoqueSolicitante).ToListAsync();
            if (produtosEstoqueMinimo.Any())
            {
                foreach (var produto in produtosEstoqueMinimo)
                {
                    var itemListFilter = listaFiltrada.Where(p => p.CodigoMV == produto.Id.ToString()).FirstOrDefault();
                    var ItemListOriginal = listaOriginal.Where(p => p.CodigoMV == produto.Id.ToString()).FirstOrDefault();

                    if (itemListFilter is not null)
                    {
                        itemListFilter.IsEstoqMin = true;
                        if (itemListFilter.Reposicao < produto.QuantidadeMinima)
                        {
                            itemListFilter.Reposicao = (int)(produto.QuantidadeMinima - itemListFilter.EstoqueAtual);
                        }
                    }
                    else //caso o produto não esteja na lista filtrada.
                    {
                        if (ItemListOriginal != null)
                        {
                            var reposicaoCalculada = (ItemListOriginal.ConsumoTotal * diasDeEstoque) - ItemListOriginal.EstoqueAtual;
                            if (reposicaoCalculada < produto.QuantidadeMinima)
                            {
                                ItemListOriginal.Reposicao = (int)(produto.QuantidadeMinima - reposicaoCalculada);
                                ItemListOriginal.IsEstoqMin = true;
                                listaFiltrada.Add(ItemListOriginal);
                            }
                            else
                            {
                                ItemListOriginal.Reposicao = (int)reposicaoCalculada;
                                ItemListOriginal.IsEstoqMin = true;
                                listaFiltrada.Add(ItemListOriginal);
                            }
                        }
                        else
                        {
                            ProdutosForaDasLista.Add(produto);
                        }
                    }
                }
            }
            return listaFiltrada;
        }
    }
}
