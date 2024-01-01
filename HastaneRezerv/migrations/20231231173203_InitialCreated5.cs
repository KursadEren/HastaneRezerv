using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HastaneRezerv.Migrations
{
    public partial class InitialCreated5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "saatstring",
                table: "Randevu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tarihstring",
                table: "Randevu",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "saatstring",
                table: "Randevu");

            migrationBuilder.DropColumn(
                name: "tarihstring",
                table: "Randevu");
        }
    }
}
