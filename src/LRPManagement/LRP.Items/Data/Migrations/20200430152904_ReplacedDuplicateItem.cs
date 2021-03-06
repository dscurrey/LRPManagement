﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Items.Migrations
{
    public partial class ReplacedDuplicateItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData
            (
                "Craftables",
                "Id",
                26,
                new[] {"Effect", "Form", "Name", "Requirement"},
                new object[]
                {
                    "Three times per day when you use the anointing skill you may restore a spent hero point to your target rather than creating a personal aura.",
                    "Talisaman, Ceremonial Regalia", "Fireglass", "Dedication"
                }
            );

            migrationBuilder.UpdateData
            (
                "Craftables",
                "Id",
                27,
                "Materials",
                "9 Tempest Jade, 7 Orichalcum, 5 Green Iron, 10 Dragonbone, 1 Month"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData
            (
                "Craftables",
                "Id",
                26,
                new[] {"Effect", "Form", "Name", "Requirement"},
                new object[]
                {
                    "Twice per day when you cast the venom spell, you may do so without spending any mana. You must be able to cast the venom spell to use this power. ",
                    "Armour, Mage", "Wyvernsting Spaulders", "Magician, Battle Mage"
                }
            );

            migrationBuilder.UpdateData
            (
                "Craftables",
                "Id",
                27,
                "Materials",
                "7 Orichalcum, 7 Weltsilver, 13 Dragonbone, 11 Beggar's Lye, 12 Iridescent Gloaming, 1 Month"
            );
        }
    }
}