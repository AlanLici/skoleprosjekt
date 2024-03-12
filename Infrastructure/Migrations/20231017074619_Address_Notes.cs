using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UiS.Dat240.Lab3.Migrations
{
    /// <inheritdoc />
    public partial class Address_Notes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_LocationNotes",
                table: "Invoices",
                newName: "Address_Notes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_Notes",
                table: "Invoices",
                newName: "Address_LocationNotes");
        }
    }
}
