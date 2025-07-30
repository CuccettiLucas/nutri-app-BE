using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Nutri.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAlimentoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfesionalId",
                table: "Alimentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Alimentos",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProfesionalId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Alimentos",
                keyColumn: "Id",
                keyValue: 3,
                column: "ProfesionalId",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfesionalId",
                table: "Alimentos");
        }
    }
}
