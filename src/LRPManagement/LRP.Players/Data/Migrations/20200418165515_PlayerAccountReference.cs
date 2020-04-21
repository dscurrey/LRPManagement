using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Players.Data.Migrations
{
    public partial class PlayerAccountReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountRef",
                table: "Player",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Player",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2020, 4, 18, 17, 55, 14, 835, DateTimeKind.Local).AddTicks(9639));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountRef",
                table: "Player");

            migrationBuilder.UpdateData(
                table: "Player",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2020, 3, 6, 19, 55, 31, 264, DateTimeKind.Local).AddTicks(9356));
        }
    }
}
