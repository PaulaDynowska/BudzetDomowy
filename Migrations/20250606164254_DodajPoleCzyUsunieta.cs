using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudzetDomowyApp.Migrations
{
    /// <inheritdoc />
    public partial class DodajPoleCzyUsunieta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CzyUsunieta",
                table: "Operacje",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CzyUsunieta",
                table: "Operacje");
        }
    }
}
