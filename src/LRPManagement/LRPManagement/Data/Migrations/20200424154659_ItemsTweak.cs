using Microsoft.EntityFrameworkCore.Migrations;

namespace LRPManagement.Data.Migrations
{
    public partial class ItemsTweak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Craftables");

            migrationBuilder.AddColumn<string>(
                name: "Effect",
                table: "Craftables",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Form",
                table: "Craftables",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Materials",
                table: "Craftables",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Requirement",
                table: "Craftables",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Effect",
                table: "Craftables");

            migrationBuilder.DropColumn(
                name: "Form",
                table: "Craftables");

            migrationBuilder.DropColumn(
                name: "Materials",
                table: "Craftables");

            migrationBuilder.DropColumn(
                name: "Requirement",
                table: "Craftables");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Craftables",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
