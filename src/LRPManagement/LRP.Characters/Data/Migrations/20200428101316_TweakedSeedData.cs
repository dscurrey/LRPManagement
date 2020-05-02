using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Characters.Data.Migrations
{
    public partial class TweakedSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData
            (
                "Character",
                "Id",
                1,
                new[] {"IsActive", "IsRetired", "Name", "Xp"},
                new object[] {false, true, "Test user 1's 1st Character", 4}
            );

            migrationBuilder.UpdateData
            (
                "Character",
                "Id",
                2,
                "Name",
                "Test user 2's 1st Character"
            );

            migrationBuilder.InsertData
            (
                "Character",
                new[] {"Id", "IsActive", "IsRetired", "Name", "PlayerId", "Xp"},
                new object[] {3, true, false, "Test user 1's 2nd Character", 1, 8}
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData
            (
                "Character",
                "Id",
                3
            );

            migrationBuilder.UpdateData
            (
                "Character",
                "Id",
                1,
                new[] {"IsActive", "IsRetired", "Name", "Xp"},
                new object[] {true, false, "Test user's 1st Character'", 8}
            );

            migrationBuilder.UpdateData
            (
                "Character",
                "Id",
                2,
                "Name",
                "Test user 2's 1st Character'"
            );
        }
    }
}