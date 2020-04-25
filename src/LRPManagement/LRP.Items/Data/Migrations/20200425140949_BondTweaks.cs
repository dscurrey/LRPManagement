using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Items.Migrations
{
    public partial class BondTweaks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Bonds");

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "Bonds",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Bonds");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Bonds",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
