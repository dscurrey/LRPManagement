using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Items.Migrations
{
    public partial class BondTweaks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn
            (
                "PlayerId",
                "Bonds"
            );

            migrationBuilder.AddColumn<int>
            (
                "CharacterId",
                "Bonds",
                nullable: false,
                defaultValue: 0
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn
            (
                "CharacterId",
                "Bonds"
            );

            migrationBuilder.AddColumn<int>
            (
                "PlayerId",
                "Bonds",
                "int",
                nullable: false,
                defaultValue: 0
            );
        }
    }
}