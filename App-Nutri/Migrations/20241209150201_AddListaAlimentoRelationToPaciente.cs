using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Nutri.Migrations
{
    /// <inheritdoc />
    public partial class AddListaAlimentoRelationToPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListasAlimentos_Usuarios_UsuarioId",
                table: "ListasAlimentos");

            migrationBuilder.DropIndex(
                name: "IX_ListasAlimentos_UsuarioId",
                table: "ListasAlimentos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "ListasAlimentos");

            migrationBuilder.AddColumn<int>(
                name: "ListaAlimentoId",
                table: "Pacientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_ListaAlimentoId",
                table: "Pacientes",
                column: "ListaAlimentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_ListasAlimentos_ListaAlimentoId",
                table: "Pacientes",
                column: "ListaAlimentoId",
                principalTable: "ListasAlimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_ListasAlimentos_ListaAlimentoId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_ListaAlimentoId",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "ListaAlimentoId",
                table: "Pacientes");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "ListasAlimentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ListasAlimentos_UsuarioId",
                table: "ListasAlimentos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListasAlimentos_Usuarios_UsuarioId",
                table: "ListasAlimentos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
