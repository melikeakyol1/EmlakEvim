using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace emlakdeneme.Migrations
{
    /// <inheritdoc />
    public partial class OdemeDurumu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OdemeDurumu",
                table: "Islemler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OdemeDurumu",
                table: "Islemler");
        }
    }
}
