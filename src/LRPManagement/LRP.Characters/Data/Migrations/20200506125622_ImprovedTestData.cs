using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Characters.Data.Migrations
{
    public partial class ImprovedTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Player 1, Retired");

            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Player 2, Active");

            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Player 1, Active");

            migrationBuilder.InsertData(
                table: "Character",
                columns: new[] { "Id", "IsActive", "IsRetired", "Name", "PlayerId", "Xp" },
                values: new object[] { 4, true, false, "Player 1, Inactive", 1, 8 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Test user 1's 1st Character");

            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Test user 2's 1st Character");

            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Test user 1's 2nd Character");
        }
    }
}
