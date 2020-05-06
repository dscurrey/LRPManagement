using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Players.Data.Migrations
{
    public partial class ImprovedTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Player",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2020, 5, 6, 14, 13, 45, 664, DateTimeKind.Local).AddTicks(5858));

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "AccountRef", "DateJoined", "FirstName", "IsActive", "LastName" },
                values: new object[] { 3, "val@dcurrey.co.uk", new DateTime(2020, 5, 6, 14, 13, 45, 672, DateTimeKind.Local).AddTicks(9747), "Valdemar", true, "Karlsson" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "AccountRef", "DateJoined", "FirstName", "IsActive", "LastName" },
                values: new object[] { 2, "jsmith@dcurrey.co.uk", new DateTime(2020, 5, 6, 14, 13, 45, 672, DateTimeKind.Local).AddTicks(9684), "John", true, "Smith" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Player",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Player",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Player",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2020, 5, 4, 16, 28, 18, 635, DateTimeKind.Local).AddTicks(6193));
        }
    }
}
