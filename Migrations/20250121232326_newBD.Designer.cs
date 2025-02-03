﻿// <auto-generated />
using BlazorRepoEstoque.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorRepoEstoque.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250121232326_newBD")]
    partial class newBD
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("BlazorRepoEstoque.Models.Grupo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CodigoMV")
                        .HasColumnType("int");

                    b.Property<string>("NomeGrupo")
                        .IsRequired()
                        .HasColumnType("varchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Grupos");
                });

            modelBuilder.Entity("BlazorRepoEstoque.Models.Medicamento", b =>
                {
                    b.Property<int>("IdMedicamento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idMedicamento");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdMedicamento"));

                    b.Property<string>("NomeMedicamento")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)")
                        .HasColumnName("medicamento");

                    b.Property<string>("Unidade")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("unidade");

                    b.HasKey("IdMedicamento");

                    b.ToTable("tbmedicamento");
                });

            modelBuilder.Entity("BlazorRepoEstoque.Models.ProdutoEstoqueMinimo", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<ushort>("EstoqueSolicitante")
                        .HasColumnType("SMALLINT UNSIGNED");

                    b.Property<string>("NomeProduto")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<uint>("QuantidadeMinima")
                        .HasColumnType("MEDIUMINT UNSIGNED");

                    b.HasKey("Id", "EstoqueSolicitante");

                    b.ToTable("ProdutoEstoqueMinimo");
                });
#pragma warning restore 612, 618
        }
    }
}
