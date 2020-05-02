using Microsoft.EntityFrameworkCore.Migrations;

namespace LRPManagement.Data.Migrations
{
    public partial class FixCharacterIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>
                (
                    "Id",
                    "Characters",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int"
                )
                .Annotation("SqlServer:Identity", "1, 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>
                (
                    "Id",
                    "Characters",
                    "int",
                    nullable: false,
                    oldClrType: typeof(int)
                )
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}