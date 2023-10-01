using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyInfo.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "This is Hyundai", "Hyundai" },
                    { 2, "This is Jac", "Jac" },
                    { 3, "This is Volvo", "Volvo" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "ID", "CompanyID", "Description", "Model" },
                values: new object[,]
                {
                    { 1, 1, "This is Elantra", "Elantra" },
                    { 2, 1, "This is Kona", "Kona" },
                    { 3, 2, "This is A30", "A30" },
                    { 4, 2, "This is Heyue RS", "Heyue RS" },
                    { 5, 3, "This is XC90", "XC90" },
                    { 6, 3, "This is V60", "V60" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
