using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Skills.Data.Migrations
{
    public partial class AddSkillXpProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "XpCost",
                table: "Skill",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "XpCost" },
                values: new object[] { "Thrown", 1 });

            migrationBuilder.UpdateData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "XpCost" },
                values: new object[] { "Ambidexterity", 1 });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Name", "XpCost" },
                values: new object[,]
                {
                    { 19, "Artisan", 4 },
                    { 18, "Apothecary", 2 },
                    { 17, "Physick", 3 },
                    { 16, "Chirurgeon", 1 },
                    { 15, "Get it Together", 1 },
                    { 14, "Stay With Me", 1 },
                    { 13, "Unstoppable", 2 },
                    { 12, "Relentless", 2 },
                    { 10, "Mortal Blow", 1 },
                    { 9, "Cleaving Strike", 1 },
                    { 8, "Hero", 2 },
                    { 7, "Fortitude", 1 },
                    { 6, "Endurance", 2 },
                    { 5, "Shield", 2 },
                    { 4, "Marksman", 4 },
                    { 11, "Mighty Strikedown", 1 },
                    { 3, "Weapon Master", 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DropColumn(
                name: "XpCost",
                table: "Skill");

            migrationBuilder.UpdateData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Weapon Master");

            migrationBuilder.UpdateData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Artisan");
        }
    }
}
