using Microsoft.EntityFrameworkCore.Migrations;

namespace LRPManagement.Data.Migrations
{
    public partial class UpdateReferenceProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharRef",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "PlayerRef",
                table: "Players",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerRef",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "CharRef",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
