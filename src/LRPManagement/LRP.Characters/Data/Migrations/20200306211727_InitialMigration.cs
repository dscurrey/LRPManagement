using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Characters.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable
            (
                "Character",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsRetired = table.Column<bool>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Character", x => x.Id); }
            );

            migrationBuilder.InsertData
            (
                "Character",
                new[] {"Id", "IsActive", "IsRetired", "Name", "PlayerId"},
                new object[] {1, true, false, "Test user's 1st Character'", 1}
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable
            (
                "Character"
            );
        }
    }
}