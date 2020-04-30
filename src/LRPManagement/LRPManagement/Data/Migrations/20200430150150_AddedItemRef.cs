using Microsoft.EntityFrameworkCore.Migrations;

namespace LRPManagement.Data.Migrations
{
    public partial class AddedItemRef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemRef",
                table: "Craftables",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemRef",
                table: "Craftables");
        }
    }
}
