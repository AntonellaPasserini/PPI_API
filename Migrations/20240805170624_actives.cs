using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CRUD__PPI.Migrations
{
    /// <inheritdoc />
    public partial class actives : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Actives",
                columns: new[] { "id", "nombre", "precioUnitario", "ticker", "tipoActivo" },
                values: new object[,]
                {
                    { 1, "Apple", 177.97m, "AAPL", 1 },
                    { 2, "Alphabet Inc", 138.21m, "GOOGL", 1 },
                    { 3, "Microsoft", 329.04m, "MSFT", 1 },
                    { 4, "Coca Cola", 58.3m, "KO", 1 },
                    { 5, "Walmart", 163.42m, "WMT", 1 },
                    { 6, "BONOS ARGENTINA USD 2030 L.A", 307.4m, "AL30", 2 },
                    { 7, "Bonos Globales Argentina USD Step Up 2030", 336.1m, "GD30", 2 },
                    { 8, "Delta Pesos Clase A", 0.0181m, "Delta.Pesos", 3 },
                    { 9, "Fima Premium Clase A", 0.0317m, "Fima.Premium", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Actives",
                keyColumn: "id",
                keyValue: 9);
        }
    }
}
