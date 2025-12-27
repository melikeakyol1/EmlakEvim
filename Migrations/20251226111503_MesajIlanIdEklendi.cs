using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace emlakdeneme.Migrations
{
    /// <inheritdoc />
    public partial class MesajIlanIdEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IlanId",
                table: "Mesajlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Mesajlar_IlanId",
                table: "Mesajlar",
                column: "IlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mesajlar_Ilanlar_IlanId",
                table: "Mesajlar",
                column: "IlanId",
                principalTable: "Ilanlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mesajlar_Ilanlar_IlanId",
                table: "Mesajlar");

            migrationBuilder.DropIndex(
                name: "IX_Mesajlar_IlanId",
                table: "Mesajlar");

            migrationBuilder.DropColumn(
                name: "IlanId",
                table: "Mesajlar");
        }
    }
}
