using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HastaneRezerv.Migrations
{
    public partial class IntialCreated2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Kullanici_KullaniciId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_KullaniciId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "KullaniciId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KullaniciId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_KullaniciId",
                table: "AspNetUsers",
                column: "KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Kullanici_KullaniciId",
                table: "AspNetUsers",
                column: "KullaniciId",
                principalTable: "Kullanici",
                principalColumn: "KullaniciId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
