using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Characters.Data.Migrations
{
    public partial class TweakedSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsActive", "IsRetired", "Name", "Xp" },
                values: new object[] { false, true, "Test user 1's 1st Character", 4 });

            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Test user 2's 1st Character");

            migrationBuilder.InsertData(
                table: "Character",
                columns: new[] { "Id", "IsActive", "IsRetired", "Name", "PlayerId", "Xp" },
                values: new object[] { 3, true, false, "Test user 1's 2nd Character", 1, 8 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsActive", "IsRetired", "Name", "Xp" },
                values: new object[] { true, false, "Test user's 1st Character'", 8 });

            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Test user 2's 1st Character'");
        }
    }
}
