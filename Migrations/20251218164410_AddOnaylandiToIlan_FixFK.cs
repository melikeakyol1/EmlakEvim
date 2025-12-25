using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace emlakdeneme.Migrations
{
    /// <inheritdoc />
    public partial class AddOnaylandiToIlan_FixFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RolId",
                table: "Kullanicilar",
                type: "int",
                nullable: true);

            /*migrationBuilder.AddColumn<int>(
                name: "RolId1",
                table: "Kullanicilar",
                type: "int",
                nullable: false,
                defaultValue: 0);*/

            /*migrationBuilder.AddColumn<int>(
                name: "KullaniciId",
                table: "Ilanlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KullaniciId1",
                table: "Ilanlar",
                type: "int",
                nullable: false,
                defaultValue: 0);*/

            migrationBuilder.AddColumn<bool>(
                name: "Onaylandi",
                table: "Ilanlar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_RolId",
                table: "Kullanicilar",
                column: "RolId");

            /*migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_RolId1",
                table: "Kullanicilar",
                column: "RolId1");*/

            migrationBuilder.CreateIndex(
                name: "IX_Ilanlar_KullaniciId",
                table: "Ilanlar",
                column: "KullaniciId");

            /*migrationBuilder.CreateIndex(
                name: "IX_Ilanlar_KullaniciId1",
                table: "Ilanlar",
                column: "KullaniciId1");*/

            migrationBuilder.AddForeignKey(
                name: "FK_Ilanlar_Kullanicilar_KullaniciId",
                table: "Ilanlar",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            /*migrationBuilder.AddForeignKey(
                name: "FK_Ilanlar_Kullanicilar_KullaniciId1",
                table: "Ilanlar",
                column: "KullaniciId1",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);*/

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanicilar_Roller_RolId",
                table: "Kullanicilar",
                column: "RolId",
                principalTable: "Roller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            /*migrationBuilder.AddForeignKey(
                name: "FK_Kullanicilar_Roller_RolId1",
                table: "Kullanicilar",
                column: "RolId1",
                principalTable: "Roller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ilanlar_Kullanicilar_KullaniciId",
                table: "Ilanlar");

            /*migrationBuilder.DropForeignKey(
                name: "FK_Ilanlar_Kullanicilar_KullaniciId1",
                table: "Ilanlar");*/

            migrationBuilder.DropForeignKey(
                name: "FK_Kullanicilar_Roller_RolId",
                table: "Kullanicilar");

            /*migrationBuilder.DropForeignKey(
                name: "FK_Kullanicilar_Roller_RolId1",
                table: "Kullanicilar");*/

            migrationBuilder.DropIndex(
                name: "IX_Kullanicilar_RolId",
                table: "Kullanicilar");

            /*migrationBuilder.DropIndex(
                name: "IX_Kullanicilar_RolId1",
                table: "Kullanicilar");*/

            migrationBuilder.DropIndex(
                name: "IX_Ilanlar_KullaniciId",
                table: "Ilanlar");

            /*migrationBuilder.DropIndex(
                name: "IX_Ilanlar_KullaniciId1",
                table: "Ilanlar");*/

            migrationBuilder.DropColumn(
                name: "RolId",
                table: "Kullanicilar");

            /*migrationBuilder.DropColumn(
                name: "RolId1",
                table: "Kullanicilar");*/

            migrationBuilder.DropColumn(
                name: "KullaniciId",
                table: "Ilanlar");

            /*migrationBuilder.DropColumn(
                name: "KullaniciId1",
                table: "Ilanlar");*/

            migrationBuilder.DropColumn(
                name: "Onaylandi",
                table: "Ilanlar");
        }
    }
}
