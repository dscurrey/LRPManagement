using Microsoft.EntityFrameworkCore.Migrations;

namespace LRPManagement.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable
            (
                "Players",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table => { table.PrimaryKey("PK_Players", x => x.Id); }
            );

            migrationBuilder.CreateTable
            (
                "Skills",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Skills", x => x.Id); }
            );

            migrationBuilder.CreateTable
            (
                "Characters",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsRetired = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey
                    (
                        "FK_Characters_Players_PlayerId",
                        x => x.PlayerId,
                        "Players",
                        "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable
            (
                "CharacterSkills",
                table => new
                {
                    CharId = table.Column<int>(nullable: false),
                    SkillId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSkills", x => new {x.CharId, x.SkillId});
                    table.ForeignKey
                    (
                        "FK_CharacterSkills_Characters_CharId",
                        x => x.CharId,
                        "Characters",
                        "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey
                    (
                        "FK_CharacterSkills_Skills_SkillId",
                        x => x.SkillId,
                        "Skills",
                        "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex
            (
                "IX_Characters_PlayerId",
                "Characters",
                "PlayerId"
            );

            migrationBuilder.CreateIndex
            (
                "IX_CharacterSkills_SkillId",
                "CharacterSkills",
                "SkillId"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable
            (
                "CharacterSkills"
            );

            migrationBuilder.DropTable
            (
                "Characters"
            );

            migrationBuilder.DropTable
            (
                "Skills"
            );

            migrationBuilder.DropTable
            (
                "Players"
            );
        }
    }
}