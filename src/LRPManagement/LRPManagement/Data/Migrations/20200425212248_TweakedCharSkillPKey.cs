using Microsoft.EntityFrameworkCore.Migrations;

namespace LRPManagement.Data.Migrations
{
    public partial class TweakedCharSkillPKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey
            (
                "PK_CharacterSkills",
                "CharacterSkills"
            );

            migrationBuilder.AlterColumn<int>
                (
                    "Id",
                    "CharacterSkills",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int"
                )
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey
            (
                "PK_CharacterSkills",
                "CharacterSkills",
                "Id"
            );

            migrationBuilder.CreateIndex
            (
                "IX_CharacterSkills_CharacterId",
                "CharacterSkills",
                "CharacterId"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey
            (
                "PK_CharacterSkills",
                "CharacterSkills"
            );

            migrationBuilder.DropIndex
            (
                "IX_CharacterSkills_CharacterId",
                "CharacterSkills"
            );

            migrationBuilder.AlterColumn<int>
                (
                    "Id",
                    "CharacterSkills",
                    "int",
                    nullable: false,
                    oldClrType: typeof(int)
                )
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey
            (
                "PK_CharacterSkills",
                "CharacterSkills",
                new[] {"CharacterId", "SkillId"}
            );
        }
    }
}