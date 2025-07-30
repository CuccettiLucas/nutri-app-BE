using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Nutri.Migrations
{
    /// <inheritdoc />
    public partial class AlimentosPersonalizados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlimentoListaAlimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListaAlimentoId = table.Column<int>(type: "int", nullable: false),
                    ListaAlimentosId = table.Column<int>(type: "int", nullable: false),
                    AlimentoId = table.Column<int>(type: "int", nullable: false),
                    PorcionGramos = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlimentoListaAlimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlimentoListaAlimentos_Alimentos_AlimentoId",
                        column: x => x.AlimentoId,
                        principalTable: "Alimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlimentoListaAlimentos_ListasAlimentos_ListaAlimentosId",
                        column: x => x.ListaAlimentosId,
                        principalTable: "ListasAlimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PacienteListaPersonalizadas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    ListaAlimentosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacienteListaPersonalizadas", x => x.id);
                    table.ForeignKey(
                        name: "FK_PacienteListaPersonalizadas_ListasAlimentos_ListaAlimentosId",
                        column: x => x.ListaAlimentosId,
                        principalTable: "ListasAlimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacienteListaPersonalizadas_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "alimentoPacientePersonalizados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteListaPersonalizadaId = table.Column<int>(type: "int", nullable: false),
                    AlimentoId = table.Column<int>(type: "int", nullable: false),
                    PorcionGramosPersonalizada = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alimentoPacientePersonalizados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_alimentoPacientePersonalizados_Alimentos_AlimentoId",
                        column: x => x.AlimentoId,
                        principalTable: "Alimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_alimentoPacientePersonalizados_PacienteListaPersonalizadas_PacienteListaPersonalizadaId",
                        column: x => x.PacienteListaPersonalizadaId,
                        principalTable: "PacienteListaPersonalizadas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlimentoListaAlimentos_AlimentoId",
                table: "AlimentoListaAlimentos",
                column: "AlimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlimentoListaAlimentos_ListaAlimentosId",
                table: "AlimentoListaAlimentos",
                column: "ListaAlimentosId");

            migrationBuilder.CreateIndex(
                name: "IX_alimentoPacientePersonalizados_AlimentoId",
                table: "alimentoPacientePersonalizados",
                column: "AlimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_alimentoPacientePersonalizados_PacienteListaPersonalizadaId",
                table: "alimentoPacientePersonalizados",
                column: "PacienteListaPersonalizadaId");

            migrationBuilder.CreateIndex(
                name: "IX_PacienteListaPersonalizadas_ListaAlimentosId",
                table: "PacienteListaPersonalizadas",
                column: "ListaAlimentosId");

            migrationBuilder.CreateIndex(
                name: "IX_PacienteListaPersonalizadas_PacienteId",
                table: "PacienteListaPersonalizadas",
                column: "PacienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlimentoListaAlimentos");

            migrationBuilder.DropTable(
                name: "alimentoPacientePersonalizados");

            migrationBuilder.DropTable(
                name: "PacienteListaPersonalizadas");
        }
    }
}
