using Microsoft.EntityFrameworkCore.Migrations;

namespace LRPManagement.Data.Migrations
{
    public partial class ImproveCharacters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Characters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Characters");
        }
    }
}
