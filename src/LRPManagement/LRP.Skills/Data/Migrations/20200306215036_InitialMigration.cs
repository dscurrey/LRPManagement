using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Skills.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable
            (
                "Skill",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Skill", x => x.Id); }
            );

            migrationBuilder.InsertData
            (
                "Skill",
                new[] {"Id", "Name"},
                new object[] {1, "Weapon Master"}
            );

            migrationBuilder.InsertData
            (
                "Skill",
                new[] {"Id", "Name"},
                new object[] {2, "Artisan"}
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable
            (
                "Skill"
            );
        }
    }
}