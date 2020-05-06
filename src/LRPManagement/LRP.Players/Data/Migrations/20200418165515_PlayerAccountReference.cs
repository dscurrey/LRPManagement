using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace LRP.Players.Data.Migrations
{
    public partial class PlayerAccountReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>
            (
                "AccountRef",
                "Player",
                nullable: true
            );

            migrationBuilder.UpdateData
            (
                "Player",
                "Id",
                1,
                "DateJoined",
                new DateTime(2020, 4, 18, 17, 55, 14, 835, DateTimeKind.Local).AddTicks(9639)
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn
            (
                "AccountRef",
                "Player"
            );

            migrationBuilder.UpdateData
            (
                "Player",
                "Id",
                1,
                "DateJoined",
                new DateTime(2020, 3, 6, 19, 55, 31, 264, DateTimeKind.Local).AddTicks(9356)
            );
        }
    }
}