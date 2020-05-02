using Microsoft.EntityFrameworkCore.Migrations;

namespace LRPManagement.Data.Migrations
{
    public partial class Identities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>
                (
                    "Id",
                    "Skills",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int"
                )
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>
                (
                    "Id",
                    "Players",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int"
                )
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>
                (
                    "Id",
                    "Craftables",
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
                    "Skills",
                    "int",
                    nullable: false,
                    oldClrType: typeof(int)
                )
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>
                (
                    "Id",
                    "Players",
                    "int",
                    nullable: false,
                    oldClrType: typeof(int)
                )
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>
                (
                    "Id",
                    "Craftables",
                    "int",
                    nullable: false,
                    oldClrType: typeof(int)
                )
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}