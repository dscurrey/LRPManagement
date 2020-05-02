using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Skills.Data.Migrations
{
    public partial class AddSkillXpProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>
            (
                "XpCost",
                "Skill",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.UpdateData
            (
                "Skill",
                "Id",
                1,
                new[] {"Name", "XpCost"},
                new object[] {"Thrown", 1}
            );

            migrationBuilder.UpdateData
            (
                "Skill",
                "Id",
                2,
                new[] {"Name", "XpCost"},
                new object[] {"Ambidexterity", 1}
            );

            migrationBuilder.InsertData
            (
                "Skill",
                new[] {"Id", "Name", "XpCost"},
                new object[,]
                {
                    {19, "Artisan", 4},
                    {18, "Apothecary", 2},
                    {17, "Physick", 3},
                    {16, "Chirurgeon", 1},
                    {15, "Get it Together", 1},
                    {14, "Stay With Me", 1},
                    {13, "Unstoppable", 2},
                    {12, "Relentless", 2},
                    {10, "Mortal Blow", 1},
                    {9, "Cleaving Strike", 1},
                    {8, "Hero", 2},
                    {7, "Fortitude", 1},
                    {6, "Endurance", 2},
                    {5, "Shield", 2},
                    {4, "Marksman", 4},
                    {11, "Mighty Strikedown", 1},
                    {3, "Weapon Master", 2}
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData
            (
                "Skill",
                "Id",
                3
            );

            migrationBuilder.DeleteData
            (
                "Skill",
                "Id",
                4
            );

            migrationBuilder.DeleteData
            (
                "Skill",
                "Id",
                5
            );

            migrationBuilder.DeleteData
            (
                "Skill",
                "Id",
                6
            );

            migrationBuilder.DeleteData
            (
                "Skill",
                "Id",
                7
            );

            migrationBuilder.DeleteData
            (
                "Skill",
                "Id",
                8
            );

            migrationBuilder.DeleteData
            (
                "Skill",
                "Id",
                9
            );

            migrationBuilder.DeleteData
            (
                "Skill",
                "Id",
                10
            );

            migrationBuilder.DeleteData
            (
                "Skill",
                "Id",
                11
            );

            migrationBuilder.DeleteData
            (
                "Skill",
                "Id",
                12
            );

            migrationBuilder.DeleteData
            (
                "Skill",
                "Id",
                13
            );

            migrationBuilder.DeleteData
            (
                "Skill",
                "Id",
                14
            );

            migrationBuilder.DeleteData
            (
                "Skill",
                "Id",
                15
            );

            migrationBuilder.DeleteData
            (
                "Skill",
                "Id",
                16
            );

            migrationBuilder.DeleteData
            (
                "Skill",
                "Id",
                17
            );

            migrationBuilder.DeleteData
            (
                "Skill",
                "Id",
                18
            );

            migrationBuilder.DeleteData
            (
                "Skill",
                "Id",
                19
            );

            migrationBuilder.DropColumn
            (
                "XpCost",
                "Skill"
            );

            migrationBuilder.UpdateData
            (
                "Skill",
                "Id",
                1,
                "Name",
                "Weapon Master"
            );

            migrationBuilder.UpdateData
            (
                "Skill",
                "Id",
                2,
                "Name",
                "Artisan"
            );
        }
    }
}