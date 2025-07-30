using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Nutri.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionPacienteModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Anamnesis_Alcohol",
                table: "Pacientes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Anamnesis_CantidadComidas",
                table: "Pacientes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Anamnesis_Cocina",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Anamnesis_DietasAnteriores",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Anamnesis_Ejercicio",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Anamnesis_Enfermedades",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Anamnesis_Fumador",
                table: "Pacientes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Anamnesis_HorarioLaboral",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Anamnesis_HorariosHabituales",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Anamnesis_Medicaciones",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Anamnesis_Observaciones",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Anamnesis_PesoHabitual",
                table: "Pacientes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Anamnesis_TomaMedicacion",
                table: "Pacientes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Anamnesis_Trabajo",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anamnesis_Alcohol",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Anamnesis_CantidadComidas",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Anamnesis_Cocina",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Anamnesis_DietasAnteriores",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Anamnesis_Ejercicio",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Anamnesis_Enfermedades",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Anamnesis_Fumador",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Anamnesis_HorarioLaboral",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Anamnesis_HorariosHabituales",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Anamnesis_Medicaciones",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Anamnesis_Observaciones",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Anamnesis_PesoHabitual",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Anamnesis_TomaMedicacion",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Anamnesis_Trabajo",
                table: "Pacientes");
        }
    }
}
