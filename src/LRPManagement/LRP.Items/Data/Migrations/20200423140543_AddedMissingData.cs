using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Items.Migrations
{
    public partial class AddedMissingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData
            (
                "Craftables",
                "Id",
                1,
                "Name",
                "Apprentice's Blade"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData
            (
                "Craftables",
                "Id",
                1,
                "Name",
                null
            );
        }
    }
}