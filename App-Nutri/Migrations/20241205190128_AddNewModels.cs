using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Nutri.Migrations
{
    /// <inheritdoc />
    public partial class AddNewModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListaAlimentosId",
                table: "Alimentos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListasAlimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListasAlimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListasAlimentos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosAlimento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    AlimentoId = table.Column<int>(type: "int", nullable: false),
                    FechaConsumo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cantidad = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosAlimento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosAlimento_Alimentos_AlimentoId",
                        column: x => x.AlimentoId,
                        principalTable: "Alimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistrosAlimento_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alimentos_ListaAlimentosId",
                table: "Alimentos",
                column: "ListaAlimentosId");

            migrationBuilder.CreateIndex(
                name: "IX_ListasAlimentos_UsuarioId",
                table: "ListasAlimentos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosAlimento_AlimentoId",
                table: "RegistrosAlimento",
                column: "AlimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosAlimento_UsuarioId",
                table: "RegistrosAlimento",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alimentos_ListasAlimentos_ListaAlimentosId",
                table: "Alimentos",
                column: "ListaAlimentosId",
                principalTable: "ListasAlimentos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alimentos_ListasAlimentos_ListaAlimentosId",
                table: "Alimentos");

            migrationBuilder.DropTable(
                name: "ListasAlimentos");

            migrationBuilder.DropTable(
                name: "RegistrosAlimento");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Alimentos_ListaAlimentosId",
                table: "Alimentos");

            migrationBuilder.DropColumn(
                name: "ListaAlimentosId",
                table: "Alimentos");
        }
    }
}
