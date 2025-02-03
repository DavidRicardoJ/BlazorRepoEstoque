using BlazorRepoEstoque.Data.IRepository;
using BlazorRepoEstoque.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRepoEstoque.Data.Repositories
{
    public class GrupoRepository : IGrupoRepository
    {
        private readonly AppDbContext context;

        public GrupoRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAllGrupos(List<Grupo> grupos)
        {
            context.Set<Grupo>().AddRange(grupos);
            await context.SaveChangesAsync();
        }

        public async Task AddGrupo(Grupo grupo)
        {
            context.Set<Grupo>().Add(grupo);
            await context.SaveChangesAsync();

        }

        public async Task DeleteGrupo(string nomeGrupo)
        {
            var grupo = context.Set<Grupo>().Where(x => x.NomeGrupo == nomeGrupo).ToList();
            context.RemoveRange(grupo);
            await context.SaveChangesAsync();
        }

        public async Task DeleteItemGrupo(List<Grupo> grupoItem)
        {
            context.Grupos.RemoveRange(grupoItem);
            await context.SaveChangesAsync();
        }

        public async Task<Grupo> FindByGrupoName(string grupoName)
        {
            var grupo = await context.Grupos.AsNoTracking().FirstOrDefaultAsync(x => x.NomeGrupo.ToUpper().Equals(grupoName.ToUpper()));
            return grupo;
        }
        public async Task<List<Grupo>> GetAllGrupos()
        {
            var grupos = await context.Grupos.ToListAsync();
            return grupos;
        }

        public Task<List<Grupo>> GetGrupoByName(string nameGroup)
        {
            var grupo = context.Grupos.AsNoTracking().Where(x => x.NomeGrupo == nameGroup).ToListAsync();
            return grupo;
        }
    }

}