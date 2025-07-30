using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Nutri.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MatriculaMn",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MatriculaMp",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatriculaMn",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "MatriculaMp",
                table: "Usuarios");
        }
    }
}
