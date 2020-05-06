using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Skills.Data.Migrations
{
    public partial class TweakCharSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey
            (
                "FK_CharacterSkill_Skill_SkillId",
                "CharacterSkill"
            );

            migrationBuilder.DropPrimaryKey
            (
                "PK_CharacterSkill",
                "CharacterSkill"
            );

            migrationBuilder.RenameTable
            (
                "CharacterSkill",
                newName: "CharacterSkills"
            );

            migrationBuilder.RenameIndex
            (
                "IX_CharacterSkill_SkillId",
                table: "CharacterSkills",
                newName: "IX_CharacterSkills_SkillId"
            );

            migrationBuilder.AddPrimaryKey
            (
                "PK_CharacterSkills",
                "CharacterSkills",
                "Id"
            );

            migrationBuilder.AddForeignKey
            (
                "FK_CharacterSkills_Skill_SkillId",
                "CharacterSkills",
                "SkillId",
                "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey
            (
                "FK_CharacterSkills_Skill_SkillId",
                "CharacterSkills"
            );

            migrationBuilder.DropPrimaryKey
            (
                "PK_CharacterSkills",
                "CharacterSkills"
            );

            migrationBuilder.RenameTable
            (
                "CharacterSkills",
                newName: "CharacterSkill"
            );

            migrationBuilder.RenameIndex
            (
                "IX_CharacterSkills_SkillId",
                table: "CharacterSkill",
                newName: "IX_CharacterSkill_SkillId"
            );

            migrationBuilder.AddPrimaryKey
            (
                "PK_CharacterSkill",
                "CharacterSkill",
                "Id"
            );

            migrationBuilder.AddForeignKey
            (
                "FK_CharacterSkill_Skill_SkillId",
                "CharacterSkill",
                "SkillId",
                "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}