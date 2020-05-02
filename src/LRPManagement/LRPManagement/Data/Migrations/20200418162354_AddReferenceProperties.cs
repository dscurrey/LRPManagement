using Microsoft.EntityFrameworkCore.Migrations;

namespace LRPManagement.Data.Migrations
{
    public partial class AddReferenceProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>
            (
                "SkillRef",
                "Skills",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<int>
            (
                "CharRef",
                "Players",
                nullable: false,
                defaultValue: 0
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn
            (
                "SkillRef",
                "Skills"
            );

            migrationBuilder.DropColumn
            (
                "CharRef",
                "Players"
            );
        }
    }
}