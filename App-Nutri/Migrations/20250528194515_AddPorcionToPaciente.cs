using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Nutri.Migrations
{
    /// <inheritdoc />
    public partial class AddPorcionToPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Porcion_Carbohidratos",
                table: "Pacientes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Porcion_Fibras",
                table: "Pacientes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Porcion_Proteinas",
                table: "Pacientes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Porcion_Carbohidratos",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Porcion_Fibras",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Porcion_Proteinas",
                table: "Pacientes");
        }
    }
}
