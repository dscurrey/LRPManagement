using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Characters.Data.Migrations
{
    public partial class AddedSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData
            (
                "Character",
                "Id",
                1,
                "Xp",
                8
            );

            migrationBuilder.UpdateData
            (
                "Character",
                "Id",
                2,
                "Xp",
                8
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData
            (
                "Character",
                "Id",
                1,
                "Xp",
                0
            );

            migrationBuilder.UpdateData
            (
                "Character",
                "Id",
                2,
                "Xp",
                0
            );
        }
    }
}