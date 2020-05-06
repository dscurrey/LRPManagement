using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Players.Data.Migrations
{
    public partial class Tweaks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Player",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountRef", "DateJoined" },
                values: new object[] { "test@dcurrey.co.uk", new DateTime(2020, 5, 4, 16, 28, 18, 635, DateTimeKind.Local).AddTicks(6193) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Player",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountRef", "DateJoined" },
                values: new object[] { null, new DateTime(2020, 4, 18, 17, 55, 14, 835, DateTimeKind.Local).AddTicks(9639) });
        }
    }
}
