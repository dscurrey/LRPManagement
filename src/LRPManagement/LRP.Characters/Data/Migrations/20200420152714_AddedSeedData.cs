using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Characters.Data.Migrations
{
    public partial class AddedSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 1,
                column: "Xp",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 2,
                column: "Xp",
                value: 8);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 1,
                column: "Xp",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 2,
                column: "Xp",
                value: 0);
        }
    }
}
