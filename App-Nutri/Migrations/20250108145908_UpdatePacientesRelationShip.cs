using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Nutri.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePacientesRelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfAsignadoId",
                table: "Pacientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_ProfAsignadoId",
                table: "Pacientes",
                column: "ProfAsignadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Usuarios_ProfAsignadoId",
                table: "Pacientes",
                column: "ProfAsignadoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Usuarios_ProfAsignadoId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_ProfAsignadoId",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "ProfAsignadoId",
                table: "Pacientes");
        }
    }
}
