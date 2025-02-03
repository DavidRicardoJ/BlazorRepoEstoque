using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorRepoEstoque.Migrations
{
    /// <inheritdoc />
    public partial class newBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeGrupo = table.Column<string>(type: "varchar(60)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodigoMV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProdutoEstoqueMinimo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    EstoqueSolicitante = table.Column<ushort>(type: "SMALLINT UNSIGNED", nullable: false),
                    NomeProduto = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuantidadeMinima = table.Column<uint>(type: "MEDIUMINT UNSIGNED", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoEstoqueMinimo", x => new { x.Id, x.EstoqueSolicitante });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbmedicamento",
                columns: table => new
                {
                    idMedicamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    medicamento = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    unidade = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbmedicamento", x => x.idMedicamento);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "ProdutoEstoqueMinimo");

            migrationBuilder.DropTable(
                name: "tbmedicamento");
        }
    }
}
