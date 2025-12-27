using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace emlakdeneme.Migrations
{
    /// <inheritdoc />
    public partial class IlanIslemMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ilanlar_Kullanicilar_KullaniciId",
                table: "Ilanlar");


            migrationBuilder.DropForeignKey(
                name: "FK_Kullanicilar_Roller_RolId",
                table: "Kullanicilar");


            migrationBuilder.DropIndex(
                name: "IX_Kullanicilar_RolId",
                table: "Kullanicilar");


            migrationBuilder.DropColumn(
                name: "RolId",
                table: "Kullanicilar");

            migrationBuilder.AlterColumn<int>(
                name: "KullaniciId",
                table: "Ilanlar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "KiralandiMi",
                table: "Ilanlar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SatildiMi",
                table: "Ilanlar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Islemler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlanId = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IslemTipi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Islemler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Islemler_Ilanlar_IlanId",
                        column: x => x.IlanId,
                        principalTable: "Ilanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Islemler_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Islemler_IlanId",
                table: "Islemler",
                column: "IlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Islemler_KullaniciId",
                table: "Islemler",
                column: "KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ilanlar_Kullanicilar_KullaniciId",
                table: "Ilanlar",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ilanlar_Kullanicilar_KullaniciId",
                table: "Ilanlar");

            migrationBuilder.DropTable(
                name: "Islemler");

            migrationBuilder.DropColumn(
                name: "KiralandiMi",
                table: "Ilanlar");

            migrationBuilder.DropColumn(
                name: "SatildiMi",
                table: "Ilanlar");

            migrationBuilder.AddColumn<int>(
                name: "RolId",
                table: "Kullanicilar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "KullaniciId",
                table: "Ilanlar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);


            migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_RolId",
                table: "Kullanicilar",
                column: "RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ilanlar_Kullanicilar_KullaniciId",
                table: "Ilanlar",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanicilar_Roller_RolId",
                table: "Kullanicilar",
                column: "RolId",
                principalTable: "Roller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);


        }
    }
}