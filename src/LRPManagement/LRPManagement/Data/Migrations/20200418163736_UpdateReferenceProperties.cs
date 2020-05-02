using Microsoft.EntityFrameworkCore.Migrations;

namespace LRPManagement.Data.Migrations
{
    public partial class UpdateReferenceProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn
            (
                "CharRef",
                "Players"
            );

            migrationBuilder.AddColumn<int>
            (
                "PlayerRef",
                "Players",
                nullable: false,
                defaultValue: 0
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn
            (
                "PlayerRef",
                "Players"
            );

            migrationBuilder.AddColumn<int>
            (
                "CharRef",
                "Players",
                "int",
                nullable: false,
                defaultValue: 0
            );
        }
    }
}