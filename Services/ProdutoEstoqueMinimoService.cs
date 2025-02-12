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

        public ProdutoEstoqueMinimoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProdutoEstoqueMinimo>> GetProdutosAsync()
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
    }
}
