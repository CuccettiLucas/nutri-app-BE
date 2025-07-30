using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App_Nutri.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablesCategySubCateg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Alimentos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Alimentos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Alimentos");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Alimentos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCategoriaId",
                table: "Alimentos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategorias_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alimentos_CategoriaId",
                table: "Alimentos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Alimentos_SubCategoriaId",
                table: "Alimentos",
                column: "SubCategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategorias_CategoriaId",
                table: "SubCategorias",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alimentos_Categorias_CategoriaId",
                table: "Alimentos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Alimentos_SubCategorias_SubCategoriaId",
                table: "Alimentos",
                column: "SubCategoriaId",
                principalTable: "SubCategorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alimentos_Categorias_CategoriaId",
                table: "Alimentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Alimentos_SubCategorias_SubCategoriaId",
                table: "Alimentos");

            migrationBuilder.DropTable(
                name: "SubCategorias");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Alimentos_CategoriaId",
                table: "Alimentos");

            migrationBuilder.DropIndex(
                name: "IX_Alimentos_SubCategoriaId",
                table: "Alimentos");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Alimentos");

            migrationBuilder.DropColumn(
                name: "SubCategoriaId",
                table: "Alimentos");

            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Alimentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Alimentos",
                columns: new[] { "Id", "Calorias", "Carbohidratos", "Categoria", "Nombre", "Porcion", "ProfesionalId", "Proteinas" },
                values: new object[,]
                {
                    { 2, 52.0, 0.0, "Fruta", "Manzana", 0.0, 0, 0.0 },
                    { 3, 96.0, 0.0, "Fruta", "Banana", 0.0, 0, 0.0 }
                });
        }
    }
}
