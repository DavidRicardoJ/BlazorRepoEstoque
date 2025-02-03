using BlazorRepoEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorRepoEstoque.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<ProdutoEstoqueMinimo> ProdutoEstoqueMinimo { get; set; }
        public virtual DbSet<Medicamento> Medicamentos { get; set; }
        public virtual DbSet<Grupo> Grupos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProdutoEstoqueMinimo>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.EstoqueSolicitante });
                entity.Property(e => e.NomeProduto).IsRequired().HasMaxLength(60);
                entity.Property(e => e.QuantidadeMinima).IsRequired().HasColumnType("MEDIUMINT UNSIGNED");
                entity.Property(e => e.EstoqueSolicitante).IsRequired().HasColumnType("SMALLINT UNSIGNED");
            });

            modelBuilder.Entity<Medicamento>(entity =>
            {
                entity.HasKey(e => e.IdMedicamento);
                entity.Property(e => e.NomeMedicamento).IsRequired().HasMaxLength(120);
                entity.Property(e => e.Unidade).IsRequired().HasMaxLength(45);
            });

            modelBuilder.Entity<Grupo>(s =>
            {
                s.HasKey("Id");
                s.Property(x => x.NomeGrupo).IsRequired().HasColumnType("varchar(60)");
                s.Property(x => x.CodigoMV).IsRequired();
            });
        }
    }
}
