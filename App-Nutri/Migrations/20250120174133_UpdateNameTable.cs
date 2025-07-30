using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Nutri.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Usuarios_ProfAsignadoId",
                table: "Pacientes");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistrosAlimento_Usuarios_UsuarioId",
                table: "RegistrosAlimento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Profesionales");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profesionales",
                table: "Profesionales",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Profesionales_ProfAsignadoId",
                table: "Pacientes",
                column: "ProfAsignadoId",
                principalTable: "Profesionales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrosAlimento_Profesionales_UsuarioId",
                table: "RegistrosAlimento",
                column: "UsuarioId",
                principalTable: "Profesionales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Profesionales_ProfAsignadoId",
                table: "Pacientes");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistrosAlimento_Profesionales_UsuarioId",
                table: "RegistrosAlimento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profesionales",
                table: "Profesionales");

            migrationBuilder.RenameTable(
                name: "Profesionales",
                newName: "Usuarios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Usuarios_ProfAsignadoId",
                table: "Pacientes",
                column: "ProfAsignadoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrosAlimento_Usuarios_UsuarioId",
                table: "RegistrosAlimento",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
