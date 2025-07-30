using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Nutri.Migrations
{
    /// <inheritdoc />
    public partial class AddListaAlimentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alimentos_ListasAlimentos_ListaAlimentosId",
                table: "Alimentos");

            migrationBuilder.DropIndex(
                name: "IX_Alimentos_ListaAlimentosId",
                table: "Alimentos");

            migrationBuilder.DropColumn(
                name: "ListaAlimentosId",
                table: "Alimentos");

            migrationBuilder.CreateTable(
                name: "ListaAlimentosAlimento",
                columns: table => new
                {
                    ListaAlimentosId = table.Column<int>(type: "int", nullable: false),
                    AlimentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaAlimentosAlimento", x => new { x.ListaAlimentosId, x.AlimentoId });
                    table.ForeignKey(
                        name: "FK_ListaAlimentosAlimento_Alimentos_AlimentoId",
                        column: x => x.AlimentoId,
                        principalTable: "Alimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListaAlimentosAlimento_ListasAlimentos_ListaAlimentosId",
                        column: x => x.ListaAlimentosId,
                        principalTable: "ListasAlimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListaAlimentosAlimento_AlimentoId",
                table: "ListaAlimentosAlimento",
                column: "AlimentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListaAlimentosAlimento");

            migrationBuilder.AddColumn<int>(
                name: "ListaAlimentosId",
                table: "Alimentos",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Alimentos",
                keyColumn: "Id",
                keyValue: 2,
                column: "ListaAlimentosId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Alimentos",
                keyColumn: "Id",
                keyValue: 3,
                column: "ListaAlimentosId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Alimentos_ListaAlimentosId",
                table: "Alimentos",
                column: "ListaAlimentosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alimentos_ListasAlimentos_ListaAlimentosId",
                table: "Alimentos",
                column: "ListaAlimentosId",
                principalTable: "ListasAlimentos",
                principalColumn: "Id");
        }
    }
}
