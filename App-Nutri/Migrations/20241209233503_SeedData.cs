using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App_Nutri.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Alimentos",
                columns: new[] { "Id", "Calorias", "Carbohidratos", "Categoria", "ListaAlimentosId", "Nombre", "Porcion", "Proteinas" },
                values: new object[,]
                {
                    { 2, 52.0, 0.0, "Fruta", null, "Manzana", 0.0, 0.0 },
                    { 3, 96.0, 0.0, "Fruta", null, "Banana", 0.0, 0.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Alimentos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Alimentos",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
