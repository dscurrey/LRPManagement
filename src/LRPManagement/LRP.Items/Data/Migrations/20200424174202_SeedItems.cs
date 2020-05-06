using Microsoft.EntityFrameworkCore.Migrations;

namespace LRP.Items.Migrations
{
    public partial class SeedItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData
            (
                "Craftables",
                new[] {"Id", "Effect", "Form", "Materials", "Name", "Requirement"},
                new object[,]
                {
                    {
                        2, "Once per day, call VENOM. You are also affected.", "Weapon",
                        "7 Beggar's Lye, 5 Ambergelt, 1 Month", "Scorpion's Sting", "N/A"
                    },
                    {
                        28,
                        " A banner bonded to this gonfalon may travel to a battle that their national banner is not attending.",
                        "Gonalfon", "18 Dragonbone, 7 Green Iron, 5 Orichalcum, 9 Iridescent Gloaming, 1 Month",
                        "Mercenary Banner", "N/A"
                    },
                    {
                        27, "While wielding this standard you gain five additional personal mana", "Magical Standard",
                        "7 Orichalcum, 7 Weltsilver, 13 Dragonbone, 11 Beggar's Lye, 12 Iridescent Gloaming, 1 Month",
                        "Celestial Sigil", "N/A"
                    },
                    {
                        26,
                        "Twice per day when you cast the venom spell, you may do so without spending any mana. You must be able to cast the venom spell to use this power. ",
                        "Armour, Mage", "3 Beggar's Lye, 3 Iridescent Gloaming, 3 Weltsilver, 1 Month",
                        "Wyvernsting Spaulders", "Magician, Battle Mage"
                    },
                    {
                        25,
                        "When you perform or cooperate with the performance of the testimony skill, you may spend up to five additional doses of liao to increase the strength or the ceremony by the same amount.",
                        "Talisman, Ceremonial Regalia",
                        "5 Tempest Jade, 7 Weltsilver, 7 Ambergelt, 9 Beggar's Lye, 11 Dragonbone, 5 Iridescent Gloaming, 1 Month",
                        "Skein Bowl", "Dedication"
                    },
                    {
                        24,
                        "Three times per day when you use the physick skill you can draw a little of your own blood and use it as if it were any one herb. An apothecary cannot use this blood when creating a potion.",
                        "Talisman, Tool", "12 Weltsilver, 12 Ambergelt, 8 Beggar's Lye, 4 Iridescent Gloaming, 1 Month",
                        "Bloodcloak", "Physick"
                    },
                    {
                        23,
                        " Once per day you may use this ring to gain one additional rank of any one ritual lore for the purposes of performing a single ritual, subject to the normal rules for effective skill.",
                        "Talisman, Ritual focus",
                        "7 Orichalcum, 7 Tempest Jade, 7 Weltsilver, 7 Ambergelt, 7 Beggar's Lye, 7 Iridescent Gloaming, 1 Month",
                        "Atun's Ring", "Magician"
                    },
                    {
                        22,
                        "Twice each day with thirty seconds of appropriate roleplaying you may open a portal as if you had cast the operate portal spell. You may use this power if you are wearing armour.",
                        "Talisman, jewellery", "6 Weltsilver, 7 Beggar's Lye, 9 Irisescent Gloaming, 1 Month",
                        "Pauper's Key", "N/A"
                    },
                    {
                        21,
                        "You may spend 2 personal mana (instead of 1 hero point) to use the unstoppable skill as if you know it. You must be able to cast spells to use this power - it will not work if you are wearing any armour other than mage armour.",
                        "Talisman, Shield", "7 Green Iron, 5 Tempest Jade, 4 Dragonbone, 1 Month", "Warcaster's Oath",
                        "Shield"
                    },
                    {
                        20,
                        "Once per day when you perform or cooperate in the performance of a religious skill you may do so without using a dose of liao.",
                        "Armour, Robe", "2 Months", "Mendicant Cassock", "Dedication"
                    },
                    {
                        19,
                        "Twice per day when you cast the venom spell, you may do so without spending any mana. You must be able to cast the venom spell to use this power.",
                        "Armour, Mage", "3 Beggar's Lye, 3 Iridescent Gloaming, 3 Weltsilver, 1 Month",
                        "Wyvernsting Spaulders", "Magician, Battle Mage"
                    },
                    {
                        18,
                        "Once per day you can consume a piece of crystal mana to restore three points of spent personal mana.",
                        "Armour, Robe", "2 Months", "Crystaltender's Vestment", "Magician"
                    },
                    {
                        17, "Gain an additional 3 ranks of endurance.", "Armour, Heavy Suit",
                        "14 Orichalcum, 7 Weltsilver, 7 Ambergelt, 3 Beggar's Lye, 1 Month", "Winterborn Warmail", "N/A"
                    },
                    {
                        29,
                        "Twice per day the coven may perform a Spring ritual that does not count towards their daily limit of rituals performed.",
                        "Paraphernalia, ritual",
                        "12 Ambergelt, 9 Weltsilver, 7 Tempest Jade, 7 Beggar's Lye, 5 Dragonbone, 2 Iridescent Gloaming, 1 Month",
                        "The Fountain of Thorns", "N/A"
                    },
                    {
                        16, "Gain an additional endurance rank", "Armour, Medium Suit",
                        "4 Ambergelt, 3 Orichalcum, 1 Month", "Mithril Shirt", "N/A"
                    },
                    {
                        14,
                        "You must be dedicated to Wisdom to use this item. Once per day, while you are in an area consecrated to Wisdom, you may spend ten minutes of appropriate roleplaying that includes playing this musical instrument. Any listener who was in the area for the entire period recovers all hero points. You cannot use this ability if you are on a battlefield or in a similar stressful environment. A listener who has lost the ability to recover hero points overnight is not effected by this power.",
                        "Weapon, Instrument", "7 Ambergelt, 9 Beggar's Lye, 5 Dragonbone", "Goldwood Pipes",
                        "Dedication"
                    },
                    {
                        13,
                        "You may perform ceremonial skills other than dedication as if you were dedicated to the virtue of Vigilance.",
                        "Weapon, Icon", "5 Iridescent Gloaming, 7 Weltsilver, 9 Dragonbone, 1 Month",
                        "Icon of the Watchful", "Dedication"
                    },
                    {
                        12,
                        "When you cast, or swift cast, the operate portal spell, or the create bond spell, or perform the discern enchantment, identify ritual performance, identify magical item functions, or discern arcane mark function of detect magic, you may do so without spending any mana.",
                        "Weapon, Ritual Staff",
                        "15 Iridescent Gloaming, 9 Tempest Jade, 9 Orichalcum, 12 Beggar's Lye, 1 Month",
                        "Staff of the Magi", "Magician"
                    },
                    {
                        11, "You may cast WEAKNESS as if you know it.", "Weapon, Staff", "2 Months",
                        "Wendigo's (Draughir's) Bargain", "Magician, Battle Mage"
                    },
                    {
                        10, "When you cast the paralysis spell, you may call IMPALE rather than PARALYSE.",
                        "Weapon, Rod", "20 Beggar's Lye, 12 Ambergelt, 5 Iridescent Gloaming, 5 Tempest Jade, 1 Month",
                        "Sceptre of the Necropolis", "Magician"
                    },
                    {
                        9,
                        "Twice per day you can cast the mend spell as if you know it and without expending any mana.",
                        "Weapon, Wand", "6 Orichalcum, 3 Iridescent Gloaming, 1 Month", "Redsteel Chisel", "Magician"
                    },
                    {
                        8, "One additional hero point, one additional personal mana",
                        "Weapon, Pair, one handed and wand",
                        "5 Dragonbone, 6 Green Iron, 5 Iridescent Gloaming, 5 Orichalcum, 3 Tempest Jade, 1 Month",
                        "Trodwalker's Readiness", "Ambidexterity, Magician"
                    },
                    {
                        7, "Gain one additional hero point.", "Weapon, Bow/Crossbow",
                        "9 Green Iron, 5 Ambergelt, 1 Month", "Oathkeeper", "Marksman"
                    },
                    {
                        6, "Twice per day you may either call CLEAVE or STRIKEDOWN with this polearm.",
                        "Weapon, Polearm", "13 Green Iron, 7 Orichalcum, 5 Ambergelt, 1 month.", "Fell Iron Fury",
                        "Weapon Master"
                    },
                    {
                        5,
                        "You can call SHATTER against an item you hit with both weapons simultaneously by spending a hero point.",
                        "Weapon, Pair, One Handed", "14 Tempest Jade, 7 Beggars Lye, 12 Dragonbone, 9 Orichalcum",
                        "Bear Claws", "Ambidexterity"
                    },
                    {
                        4, "You may spend a hero point to call REPEL", "Weapon, One Handed Spear",
                        "10 Tempest Jade, 10 Ambergelt", "Sydanjaa's Call", "Weapon Master"
                    },
                    {
                        3, "Twice per day you may call IMPALE", "Weapon, Two Handed",
                        "13 Tempest Jade, 3 Orichalcum, 1 Month", "Landsknecht's Zweihänder", "Weapon Master"
                    },
                    {
                        15, "You gain an additional hero point.", "Armour, light Suit",
                        "9 Green Iron, 5 Iridescent Gloaming, 1 Month", "Runemark Shirt", "N/A"
                    },
                    {
                        30,
                        "The aura of every member of the sect is concealed from the insight ceremony. A character responding to a quick insight must respond \"my aura is concealed\" and provide no other information. ",
                        "Reliquary", "5 Orichalcum, 7 Tempest Jade, 7 Beggar's Lye, 11 Dragonbone, 1 Month",
                        "Almery of Silence", "N/A"
                    }
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                2
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                3
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                4
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                5
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                6
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                7
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                8
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                9
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                10
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                11
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                12
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                13
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                14
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                15
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                16
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                17
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                18
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                19
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                20
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                21
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                22
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                23
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                24
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                25
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                26
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                27
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                28
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                29
            );

            migrationBuilder.DeleteData
            (
                "Craftables",
                "Id",
                30
            );
        }
    }
}