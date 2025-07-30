using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Nutri.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAnamnesisModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anamnesis_HorariosHabituales",
                table: "Pacientes");

            migrationBuilder.AddColumn<int>(
                name: "Anamnesis_Talla",
                table: "Pacientes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anamnesis_Talla",
                table: "Pacientes");

            migrationBuilder.AddColumn<string>(
                name: "Anamnesis_HorariosHabituales",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
