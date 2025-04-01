using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorRepoEstoque.Migrations
{
    /// <inheritdoc />
    public partial class grupoProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GruposProdutos",
                columns: table => new
                {
                    NomeGrupoID = table.Column<int>(type: "int", nullable: false),
                    ProdutoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposProdutos", x => new { x.NomeGrupoID, x.ProdutoID });
                    table.ForeignKey(
                        name: "FK_GruposProdutos_GroupNames_NomeGrupoID",
                        column: x => x.NomeGrupoID,
                        principalTable: "GroupNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GruposProdutos_tbmedicamento_ProdutoID",
                        column: x => x.ProdutoID,
                        principalTable: "tbmedicamento",
                        principalColumn: "idMedicamento",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GruposProdutos_ProdutoID",
                table: "GruposProdutos",
                column: "ProdutoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GruposProdutos");
        }
    }
}
