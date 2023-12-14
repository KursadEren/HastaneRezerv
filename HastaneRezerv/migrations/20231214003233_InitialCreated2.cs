using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HastaneRezerv.Migrations
{
    public partial class InitialCreated2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TekrarSifre",
                table: "Kullanici",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TekrarSifre",
                table: "Kullanici");
        }
    }
}
