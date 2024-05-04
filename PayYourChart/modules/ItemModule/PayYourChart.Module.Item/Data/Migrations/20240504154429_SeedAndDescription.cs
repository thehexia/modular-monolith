using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PayYourChart.Module.Item.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedAndDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Item",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "Description", "ItemCode", "Price" },
                values: new object[,]
                {
                    { 1L, "Speech/hearing therapy", "92507", 500.25m },
                    { 2L, "Use of speech device service", "92609", 205.75m },
                    { 3L, "Cardiac rehab", "93797", 12345.67m },
                    { 4L, "Cardiac rehab/monitor", "93798", 789.10m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Item");
        }
    }
}
