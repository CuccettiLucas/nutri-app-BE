using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Nutri.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSubCategoriaPersonalizada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PorcionGramos",
                table: "SubCategorias",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "SubCategoriaPacientePersonalizadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteListaPersonalizadaId = table.Column<int>(type: "int", nullable: false),
                    SubCategoriaId = table.Column<int>(type: "int", nullable: false),
                    PorcionGramosPersonalizada = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoriaPacientePersonalizadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategoriaPacientePersonalizadas_PacienteListaPersonalizadas_PacienteListaPersonalizadaId",
                        column: x => x.PacienteListaPersonalizadaId,
                        principalTable: "PacienteListaPersonalizadas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubCategoriaPacientePersonalizadas_SubCategorias_SubCategoriaId",
                        column: x => x.SubCategoriaId,
                        principalTable: "SubCategorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoriaPacientePersonalizadas_PacienteListaPersonalizadaId",
                table: "SubCategoriaPacientePersonalizadas",
                column: "PacienteListaPersonalizadaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoriaPacientePersonalizadas_SubCategoriaId",
                table: "SubCategoriaPacientePersonalizadas",
                column: "SubCategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubCategoriaPacientePersonalizadas");

            migrationBuilder.DropColumn(
                name: "PorcionGramos",
                table: "SubCategorias");
        }
    }
}
