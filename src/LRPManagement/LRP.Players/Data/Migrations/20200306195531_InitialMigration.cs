using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Players.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DateJoined = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "DateJoined", "FirstName", "IsActive", "LastName" },
                values: new object[] { 1, new DateTime(2020, 3, 6, 19, 55, 31, 264, DateTimeKind.Local).AddTicks(9356), "Test", true, "user" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
