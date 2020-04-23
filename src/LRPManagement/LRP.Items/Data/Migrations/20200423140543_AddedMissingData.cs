using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Items.Migrations
{
    public partial class AddedMissingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Craftables",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Apprentice's Blade");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Craftables",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: null);
        }
    }
}
