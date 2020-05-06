using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Skills.Data.Migrations
{
    public partial class AddedCharSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable
            (
                "CharacterSkill",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<int>(nullable: false),
                    SkillId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSkill", x => x.Id);
                    table.ForeignKey
                    (
                        "FK_CharacterSkill_Skill_SkillId",
                        x => x.SkillId,
                        "Skill",
                        "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex
            (
                "IX_CharacterSkill_SkillId",
                "CharacterSkill",
                "SkillId"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable
            (
                "CharacterSkill"
            );
        }
    }
}