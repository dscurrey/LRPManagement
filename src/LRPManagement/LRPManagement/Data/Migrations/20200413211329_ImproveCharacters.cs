using Microsoft.EntityFrameworkCore.Migrations;

namespace LRPManagement.Data.Migrations
{
    public partial class ImproveCharacters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>
            (
                "Name",
                "Players",
                nullable: true
            );

            migrationBuilder.AddColumn<string>
            (
                "Name",
                "Characters",
                nullable: true
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn
            (
                "Name",
                "Players"
            );

            migrationBuilder.DropColumn
            (
                "Name",
                "Characters"
            );
        }
    }
}