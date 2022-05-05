using BlazorRepoEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorRepoEstoque.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Grupo> Grupos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grupo>(s =>
            {
                s.HasKey("Id");
                s.Property(x => x.NomeGrupo).IsRequired();
                s.Property(x => x.CodigoMV).IsRequired();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }


    }
}
