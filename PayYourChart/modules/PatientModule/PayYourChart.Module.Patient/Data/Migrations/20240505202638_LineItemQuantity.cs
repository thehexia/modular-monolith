using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayYourChart.Module.Patient.Data.Migrations
{
    /// <inheritdoc />
    public partial class LineItemQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Quantity",
                table: "LineItem",
                type: "INTEGER",
                nullable: false,
                defaultValue: (short)1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "LineItem");
        }
    }
}
