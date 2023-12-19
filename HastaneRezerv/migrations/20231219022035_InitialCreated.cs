using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HastaneRezerv.Migrations
{
    public partial class InitialCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AktiflikId",
                table: "Randevu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AktiflikId",
                table: "Poliklinik",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AktiflikId",
                table: "Kullanici",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AktiflikId",
                table: "Hastane",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AktiflikId",
                table: "Doktor",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AktiflikId",
                table: "AnaBilimDali",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_AktiflikId",
                table: "Randevu",
                column: "AktiflikId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_DoktorId",
                table: "Randevu",
                column: "DoktorId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_KullaniciId",
                table: "Randevu",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Poliklinik_AktiflikId",
                table: "Poliklinik",
                column: "AktiflikId");

            migrationBuilder.CreateIndex(
                name: "IX_Kullanici_AktiflikId",
                table: "Kullanici",
                column: "AktiflikId");

            migrationBuilder.CreateIndex(
                name: "IX_Hastane_AktiflikId",
                table: "Hastane",
                column: "AktiflikId");

            migrationBuilder.CreateIndex(
                name: "IX_Doktor_AktiflikId",
                table: "Doktor",
                column: "AktiflikId");

            migrationBuilder.CreateIndex(
                name: "IX_AnaBilimDali_AktiflikId",
                table: "AnaBilimDali",
                column: "AktiflikId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnaBilimDali_Aktiflik_AktiflikId",
                table: "AnaBilimDali",
                column: "AktiflikId",
                principalTable: "Aktiflik",
                principalColumn: "AktiflikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doktor_Aktiflik_AktiflikId",
                table: "Doktor",
                column: "AktiflikId",
                principalTable: "Aktiflik",
                principalColumn: "AktiflikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hastane_Aktiflik_AktiflikId",
                table: "Hastane",
                column: "AktiflikId",
                principalTable: "Aktiflik",
                principalColumn: "AktiflikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanici_Aktiflik_AktiflikId",
                table: "Kullanici",
                column: "AktiflikId",
                principalTable: "Aktiflik",
                principalColumn: "AktiflikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Poliklinik_Aktiflik_AktiflikId",
                table: "Poliklinik",
                column: "AktiflikId",
                principalTable: "Aktiflik",
                principalColumn: "AktiflikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevu_Aktiflik_AktiflikId",
                table: "Randevu",
                column: "AktiflikId",
                principalTable: "Aktiflik",
                principalColumn: "AktiflikId"
                );

            migrationBuilder.AddForeignKey(
                name: "FK_Randevu_Doktor_DoktorId",
                table: "Randevu",
                column: "DoktorId",
                principalTable: "Doktor",
                principalColumn: "DoktorId"
               );

            migrationBuilder.AddForeignKey(
                name: "FK_Randevu_Kullanici_KullaniciId",
                table: "Randevu",
                column: "KullaniciId",
                principalTable: "Kullanici",
                principalColumn: "KullaniciId"
               );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnaBilimDali_Aktiflik_AktiflikId",
                table: "AnaBilimDali");

            migrationBuilder.DropForeignKey(
                name: "FK_Doktor_Aktiflik_AktiflikId",
                table: "Doktor");

            migrationBuilder.DropForeignKey(
                name: "FK_Hastane_Aktiflik_AktiflikId",
                table: "Hastane");

            migrationBuilder.DropForeignKey(
                name: "FK_Kullanici_Aktiflik_AktiflikId",
                table: "Kullanici");

            migrationBuilder.DropForeignKey(
                name: "FK_Poliklinik_Aktiflik_AktiflikId",
                table: "Poliklinik");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevu_Aktiflik_AktiflikId",
                table: "Randevu");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevu_Doktor_DoktorId",
                table: "Randevu");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevu_Kullanici_KullaniciId",
                table: "Randevu");

            migrationBuilder.DropIndex(
                name: "IX_Randevu_AktiflikId",
                table: "Randevu");

            migrationBuilder.DropIndex(
                name: "IX_Randevu_DoktorId",
                table: "Randevu");

            migrationBuilder.DropIndex(
                name: "IX_Randevu_KullaniciId",
                table: "Randevu");

            migrationBuilder.DropIndex(
                name: "IX_Poliklinik_AktiflikId",
                table: "Poliklinik");

            migrationBuilder.DropIndex(
                name: "IX_Kullanici_AktiflikId",
                table: "Kullanici");

            migrationBuilder.DropIndex(
                name: "IX_Hastane_AktiflikId",
                table: "Hastane");

            migrationBuilder.DropIndex(
                name: "IX_Doktor_AktiflikId",
                table: "Doktor");

            migrationBuilder.DropIndex(
                name: "IX_AnaBilimDali_AktiflikId",
                table: "AnaBilimDali");

            migrationBuilder.DropColumn(
                name: "AktiflikId",
                table: "Randevu");

            migrationBuilder.DropColumn(
                name: "AktiflikId",
                table: "Poliklinik");

            migrationBuilder.DropColumn(
                name: "AktiflikId",
                table: "Kullanici");

            migrationBuilder.DropColumn(
                name: "AktiflikId",
                table: "Hastane");

            migrationBuilder.DropColumn(
                name: "AktiflikId",
                table: "Doktor");

            migrationBuilder.DropColumn(
                name: "AktiflikId",
                table: "AnaBilimDali");
        }
    }
}
