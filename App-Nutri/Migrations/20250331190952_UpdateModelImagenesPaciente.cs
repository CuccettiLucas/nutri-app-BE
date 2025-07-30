using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Nutri.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelImagenesPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comida",
                table: "ImagenesPacientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comida",
                table: "ImagenesPacientes");
        }
    }
}
