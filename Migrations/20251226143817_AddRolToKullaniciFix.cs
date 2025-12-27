using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace emlakdeneme.Migrations
{
    /// <inheritdoc />
    public partial class AddRolToKullaniciFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Önce nullable olarak ekle (Hata almamak için)
            migrationBuilder.AddColumn<int>(
                name: "RolId",
                table: "Kullanicilar",
                type: "int",
                nullable: true);

            // 2. Roller tablosuna varsayılan rolleri ekle (SQL Komutu ile)
            // Eğer Id'leri elle veriyorsan IDENTITY_INSERT kullanman gerekebilir
            migrationBuilder.Sql("INSERT INTO Roller (Ad) VALUES ('Admin')");
            migrationBuilder.Sql("INSERT INTO Roller (Ad) VALUES ('Kullanıcı')");

            // 3. Mevcut tüm kullanıcıları 'Kullanıcı' rolüne (Id'si 2 olan) ata
            migrationBuilder.Sql("UPDATE Kullanicilar SET RolId = (SELECT TOP 1 Id FROM Roller WHERE Ad = 'Kullanıcı')");

            // 4. Şimdi sütunu nullable: false (Zorunlu) ve Foreign Key yapabiliriz
            migrationBuilder.AlterColumn<int>(
                name: "RolId",
                table: "Kullanicilar",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_RolId",
                table: "Kullanicilar",
                column: "RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanicilar_Roller_RolId",
                table: "Kullanicilar",
                column: "RolId",
                principalTable: "Roller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kullanicilar_Roller_RolId",
                table: "Kullanicilar");

            migrationBuilder.DropIndex(
                name: "IX_Kullanicilar_RolId",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "RolId",
                table: "Kullanicilar");
        }
    }
}
