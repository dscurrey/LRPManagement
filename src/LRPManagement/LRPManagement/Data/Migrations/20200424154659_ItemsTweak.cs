using Microsoft.EntityFrameworkCore.Migrations;

namespace LRPManagement.Data.Migrations
{
    public partial class ItemsTweak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn
            (
                "Type",
                "Craftables"
            );

            migrationBuilder.AddColumn<string>
            (
                "Effect",
                "Craftables",
                nullable: true
            );

            migrationBuilder.AddColumn<string>
            (
                "Form",
                "Craftables",
                nullable: true
            );

            migrationBuilder.AddColumn<string>
            (
                "Materials",
                "Craftables",
                nullable: true
            );

            migrationBuilder.AddColumn<string>
            (
                "Requirement",
                "Craftables",
                nullable: true
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn
            (
                "Effect",
                "Craftables"
            );

            migrationBuilder.DropColumn
            (
                "Form",
                "Craftables"
            );

            migrationBuilder.DropColumn
            (
                "Materials",
                "Craftables"
            );

            migrationBuilder.DropColumn
            (
                "Requirement",
                "Craftables"
            );

            migrationBuilder.AddColumn<string>
            (
                "Type",
                "Craftables",
                "nvarchar(max)",
                nullable: true
            );
        }
    }
}