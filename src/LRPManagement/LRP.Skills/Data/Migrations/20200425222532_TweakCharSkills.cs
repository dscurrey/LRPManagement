using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Skills.Data.Migrations
{
    public partial class TweakCharSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterSkill_Skill_SkillId",
                table: "CharacterSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterSkill",
                table: "CharacterSkill");

            migrationBuilder.RenameTable(
                name: "CharacterSkill",
                newName: "CharacterSkills");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterSkill_SkillId",
                table: "CharacterSkills",
                newName: "IX_CharacterSkills_SkillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterSkills",
                table: "CharacterSkills",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterSkills_Skill_SkillId",
                table: "CharacterSkills",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterSkills_Skill_SkillId",
                table: "CharacterSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterSkills",
                table: "CharacterSkills");

            migrationBuilder.RenameTable(
                name: "CharacterSkills",
                newName: "CharacterSkill");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterSkills_SkillId",
                table: "CharacterSkill",
                newName: "IX_CharacterSkill_SkillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterSkill",
                table: "CharacterSkill",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterSkill_Skill_SkillId",
                table: "CharacterSkill",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
