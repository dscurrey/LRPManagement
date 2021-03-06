﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace LRPManagement.Data.Migrations
{
    public partial class AddSkillXpProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>
            (
                "XpCost",
                "Skills",
                nullable: false,
                defaultValue: 0
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn
            (
                "XpCost",
                "Skills"
            );
        }
    }
}