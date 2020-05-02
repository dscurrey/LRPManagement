using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Items.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable
            (
                "Craftables",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Form = table.Column<string>(nullable: true),
                    Requirement = table.Column<string>(nullable: true),
                    Effect = table.Column<string>(nullable: true),
                    Materials = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Craftables", x => x.Id); }
            );

            migrationBuilder.InsertData
            (
                "Craftables",
                new[] {"Id", "Effect", "Form", "Materials", "Name", "Requirement"},
                new object[] {1, "Spend one hero point to call CLEAVE", "Weapon, One Handed", "N/A", null, "N/A"}
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable
            (
                "Craftables"
            );
        }
    }
}