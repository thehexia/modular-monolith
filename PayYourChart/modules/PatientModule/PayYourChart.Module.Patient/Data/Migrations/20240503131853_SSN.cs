using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayYourChart.Module.Patient.Data.Migrations
{
    /// <inheritdoc />
    public partial class SSN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SSN",
                table: "Patient",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_SSN_DateOfBirth",
                table: "Patient",
                columns: new[] { "SSN", "DateOfBirth" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patient_SSN_DateOfBirth",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "SSN",
                table: "Patient");
        }
    }
}
