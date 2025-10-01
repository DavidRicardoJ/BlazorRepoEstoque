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
        public virtual DbSet<Grupo> GroupNames { get; set; }
        public virtual DbSet<GrupoProduto> GruposProdutos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProdutoEstoqueMinimo>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.EstoqueSolicitante, e.EstoqueOrigem });
                entity.Property(e => e.NomeProduto).IsRequired().HasMaxLength(255);
                entity.Property(e => e.QuantidadeMinima).IsRequired().HasColumnType("MEDIUMINT UNSIGNED");
                entity.Property(e => e.EstoqueSolicitante).IsRequired().HasColumnType("SMALLINT UNSIGNED");
                entity.Property(e => e.EstoqueOrigem).IsRequired().HasColumnType("SMALLINT UNSIGNED");
            });

            modelBuilder.Entity<Medicamento>(entity =>
            {
                entity.HasKey(e => e.IdMedicamento);
                entity.Property(e => e.NomeMedicamento).IsRequired().HasMaxLength(120);
                entity.Property(e => e.Unidade).IsRequired().HasMaxLength(45);
            });

            modelBuilder.Entity<Grupo>(Entity =>
            {
                Entity.HasKey(e => e.Id);
                Entity.Property(e => e.Id).ValueGeneratedOnAdd();
                Entity.Property(e => e.GroupName).IsRequired().HasMaxLength(80);               
            });

            // Configurar o relacionamento entre NomeGrupo e GrupoProduto
            modelBuilder.Entity<GrupoProduto>()
                .HasOne(gp => gp.Grupo)
                .WithMany(ng => ng.GruposProdutos)
                .HasForeignKey(gp => gp.NomeGrupoID);

            // Configurar o relacionamento entre Medicamento e GrupoProduto
            modelBuilder.Entity<GrupoProduto>()
                .HasOne(gp => gp.Produto)
                .WithMany(m => m.GruposProdutos)
                .HasForeignKey(gp => gp.ProdutoID);
            modelBuilder.Entity<GrupoProduto>().HasKey(gp => new { gp.NomeGrupoID, gp.ProdutoID });
        }
    }
}
