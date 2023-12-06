using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HastaneRezerv.Migrations
{
    public partial class InitialCreated3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aktiflik",
                columns: table => new
                {
                    AktiflikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Durum = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aktiflik", x => x.AktiflikId);
                });

            migrationBuilder.CreateTable(
                name: "AnaBilimDali",
                columns: table => new
                {
                    AnaBilimDaliId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnaBilimDaliAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnaBilimDali", x => x.AnaBilimDaliId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Poliklinik_HastaneId",
                table: "Poliklinik",
                column: "HastaneId");

            migrationBuilder.CreateIndex(
                name: "IX_Kullanici_UnvanId",
                table: "Kullanici",
                column: "UnvanId");

            migrationBuilder.CreateIndex(
                name: "IX_Doktor_AnaBilimDaliId",
                table: "Doktor",
                column: "AnaBilimDaliId");

            migrationBuilder.CreateIndex(
                name: "IX_Doktor_PoliklinikId",
                table: "Doktor",
                column: "PoliklinikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doktor_AnaBilimDali_AnaBilimDaliId",
                table: "Doktor",
                column: "AnaBilimDaliId",
                principalTable: "AnaBilimDali",
                principalColumn: "AnaBilimDaliId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doktor_Poliklinik_PoliklinikId",
                table: "Doktor",
                column: "PoliklinikId",
                principalTable: "Poliklinik",
                principalColumn: "PoliklinikId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanici_Unvan_UnvanId",
                table: "Kullanici",
                column: "UnvanId",
                principalTable: "Unvan",
                principalColumn: "UnvanId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Poliklinik_Hastane_HastaneId",
                table: "Poliklinik",
                column: "HastaneId",
                principalTable: "Hastane",
                principalColumn: "HastaneId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doktor_AnaBilimDali_AnaBilimDaliId",
                table: "Doktor");

            migrationBuilder.DropForeignKey(
                name: "FK_Doktor_Poliklinik_PoliklinikId",
                table: "Doktor");

            migrationBuilder.DropForeignKey(
                name: "FK_Kullanici_Unvan_UnvanId",
                table: "Kullanici");

            migrationBuilder.DropForeignKey(
                name: "FK_Poliklinik_Hastane_HastaneId",
                table: "Poliklinik");

            migrationBuilder.DropTable(
                name: "Aktiflik");

            migrationBuilder.DropTable(
                name: "AnaBilimDali");

            migrationBuilder.DropIndex(
                name: "IX_Poliklinik_HastaneId",
                table: "Poliklinik");

            migrationBuilder.DropIndex(
                name: "IX_Kullanici_UnvanId",
                table: "Kullanici");

            migrationBuilder.DropIndex(
                name: "IX_Doktor_AnaBilimDaliId",
                table: "Doktor");

            migrationBuilder.DropIndex(
                name: "IX_Doktor_PoliklinikId",
                table: "Doktor");
        }
    }
}
