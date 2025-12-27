using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace emlakdeneme.Migrations
{
    /// <inheritdoc />
    public partial class KullanıcıGüncellendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adres",
                table: "Kullanicilar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sehir",
                table: "Kullanicilar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "Kullanicilar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adres",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "Sehir",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "Kullanicilar");
        }
    }
}
