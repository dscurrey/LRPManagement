using Microsoft.EntityFrameworkCore.Migrations;

namespace LRPManagement.Data.Migrations
{
    public partial class AddedItemRef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>
            (
                "ItemRef",
                "Craftables",
                nullable: false,
                defaultValue: 0
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn
            (
                "ItemRef",
                "Craftables"
            );
        }
    }
}