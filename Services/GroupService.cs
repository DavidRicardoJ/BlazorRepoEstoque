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
    public class NomeGrupoService : IGroupService
    {
        private readonly AppDbContext _context;

        public NomeGrupoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Grupo>> GetNomeGruposAsync()
        {
            return await _context.GroupNames.ToListAsync();
        }

        public async Task<Grupo> GetNomeGrupoByIdAsync(int id)
        {
            return await _context.GroupNames.FindAsync(id);
        }

        public async Task<Grupo> CreateNomeGrupoAsync(Grupo nomeGrupo)
        {
            nomeGrupo.GroupName = nomeGrupo.GroupName.ToUpper().Trim();
            _context.GroupNames.Add(nomeGrupo);
            await _context.SaveChangesAsync();
            return nomeGrupo;
        }

        public async Task<Grupo> UpdateNomeGrupoAsync(Grupo nomeGrupo)
        {
            // Correção do problema de tracking
            var existingEntity = await _context.GroupNames.FindAsync(nomeGrupo.Id);
            if (existingEntity == null)
            {
                throw new Exception($"Grupo com ID {nomeGrupo.Id} não encontrado.");
            }

            // Atualiza apenas as propriedades necessárias
            _context.Entry(existingEntity).CurrentValues.SetValues(nomeGrupo);
            // Ou alternativamente:
            // existingEntity.GroupName = nomeGrupo.GroupName;

            await _context.SaveChangesAsync();
            return existingEntity;
        }

        public async Task DeleteNomeGrupoAsync(int id)
        {
            var nomeGrupo = await _context.GroupNames.FindAsync(id);
            if (nomeGrupo != null)
            {
                _context.GroupNames.Remove(nomeGrupo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddGrupoProduto(int? nomeGrupoId, int produtoId)
        {
            var grupoProduto = new GrupoProduto
            {
                NomeGrupoID = (int)nomeGrupoId,
                ProdutoID = produtoId
            };

            _context.GruposProdutos.Add(grupoProduto);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveGrupoProduto(int? nomeGrupoId, int produtoId)
        {
            var grupoProduto = await _context.GruposProdutos
                .FirstOrDefaultAsync(gp => gp.NomeGrupoID == nomeGrupoId && gp.ProdutoID == produtoId);
            if (grupoProduto != null)
            {
                _context.GruposProdutos.Remove(grupoProduto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<GrupoProduto>> GetGrupoProdutosByGroupId(int nomeGrupoId)
        {
            return await _context.GruposProdutos
        .Where(gp => gp.NomeGrupoID == nomeGrupoId)
        .Include(gp => gp.Grupo)  // Carrega o grupo relacionado
        .Include(gp => gp.Produto)    // Carrega o produto/medicamento relacionado
        .ToListAsync();
        }

        public async Task<List<GrupoProduto>> GetAllGrupoProdutos()
        {
            return await _context.GruposProdutos        
        .Include(gp => gp.Grupo)  // Carrega o grupo relacionado
        .Include(gp => gp.Produto)    // Carrega o produto/medicamento relacionado
        .ToListAsync();
        }
    }
}