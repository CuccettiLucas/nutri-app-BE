using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Nutri.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelacionListaAlimentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListaAlimentosAlimento_Alimentos_AlimentoId",
                table: "ListaAlimentosAlimento");

            migrationBuilder.DropForeignKey(
                name: "FK_ListaAlimentosAlimento_ListasAlimentos_ListaAlimentosId",
                table: "ListaAlimentosAlimento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListaAlimentosAlimento",
                table: "ListaAlimentosAlimento");

            migrationBuilder.RenameTable(
                name: "ListaAlimentosAlimento",
                newName: "ListaAlimentosAlimentos");

            migrationBuilder.RenameIndex(
                name: "IX_ListaAlimentosAlimento_AlimentoId",
                table: "ListaAlimentosAlimentos",
                newName: "IX_ListaAlimentosAlimentos_AlimentoId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ListaAlimentosAlimentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListaAlimentosAlimentos",
                table: "ListaAlimentosAlimentos",
                columns: new[] { "ListaAlimentosId", "AlimentoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ListaAlimentosAlimentos_Alimentos_AlimentoId",
                table: "ListaAlimentosAlimentos",
                column: "AlimentoId",
                principalTable: "Alimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListaAlimentosAlimentos_ListasAlimentos_ListaAlimentosId",
                table: "ListaAlimentosAlimentos",
                column: "ListaAlimentosId",
                principalTable: "ListasAlimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListaAlimentosAlimentos_Alimentos_AlimentoId",
                table: "ListaAlimentosAlimentos");

            migrationBuilder.DropForeignKey(
                name: "FK_ListaAlimentosAlimentos_ListasAlimentos_ListaAlimentosId",
                table: "ListaAlimentosAlimentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListaAlimentosAlimentos",
                table: "ListaAlimentosAlimentos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ListaAlimentosAlimentos");

            migrationBuilder.RenameTable(
                name: "ListaAlimentosAlimentos",
                newName: "ListaAlimentosAlimento");

            migrationBuilder.RenameIndex(
                name: "IX_ListaAlimentosAlimentos_AlimentoId",
                table: "ListaAlimentosAlimento",
                newName: "IX_ListaAlimentosAlimento_AlimentoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListaAlimentosAlimento",
                table: "ListaAlimentosAlimento",
                columns: new[] { "ListaAlimentosId", "AlimentoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ListaAlimentosAlimento_Alimentos_AlimentoId",
                table: "ListaAlimentosAlimento",
                column: "AlimentoId",
                principalTable: "Alimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListaAlimentosAlimento_ListasAlimentos_ListaAlimentosId",
                table: "ListaAlimentosAlimento",
                column: "ListaAlimentosId",
                principalTable: "ListasAlimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
