using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Characters.Data.Migrations
{
    public partial class ExtraDemoData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Character",
                columns: new[] { "Id", "IsActive", "IsRetired", "Name", "PlayerId" },
                values: new object[] { 2, true, false, "Test user 2's 1st Character'", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
