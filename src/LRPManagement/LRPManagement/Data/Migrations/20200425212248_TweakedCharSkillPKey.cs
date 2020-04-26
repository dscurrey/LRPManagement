using Microsoft.EntityFrameworkCore.Migrations;

namespace LRPManagement.Data.Migrations
{
    public partial class TweakedCharSkillPKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterSkills",
                table: "CharacterSkills");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CharacterSkills",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterSkills",
                table: "CharacterSkills",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkills_CharacterId",
                table: "CharacterSkills",
                column: "CharacterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterSkills",
                table: "CharacterSkills");

            migrationBuilder.DropIndex(
                name: "IX_CharacterSkills_CharacterId",
                table: "CharacterSkills");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CharacterSkills",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterSkills",
                table: "CharacterSkills",
                columns: new[] { "CharacterId", "SkillId" });
        }
    }
}
