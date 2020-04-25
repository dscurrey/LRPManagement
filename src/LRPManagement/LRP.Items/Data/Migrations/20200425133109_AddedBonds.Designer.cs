﻿// <auto-generated />
using LRP.Items.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LRP.Items.Migrations
{
    [DbContext(typeof(ItemsDbContext))]
    [Migration("20200425133109_AddedBonds")]
    partial class AddedBonds
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LRP.Items.Models.Bond", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("Bonds");
                });

            modelBuilder.Entity("LRP.Items.Models.Craftable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Effect")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Form")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Materials")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Requirement")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Craftables");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Effect = "Spend one hero point to call CLEAVE",
                            Form = "Weapon, One Handed",
                            Materials = "N/A",
                            Name = "Apprentice's Blade",
                            Requirement = "N/A"
                        },
                        new
                        {
                            Id = 2,
                            Effect = "Once per day, call VENOM. You are also affected.",
                            Form = "Weapon",
                            Materials = "7 Beggar's Lye, 5 Ambergelt, 1 Month",
                            Name = "Scorpion's Sting",
                            Requirement = "N/A"
                        },
                        new
                        {
                            Id = 3,
                            Effect = "Twice per day you may call IMPALE",
                            Form = "Weapon, Two Handed",
                            Materials = "13 Tempest Jade, 3 Orichalcum, 1 Month",
                            Name = "Landsknecht's Zweihänder",
                            Requirement = "Weapon Master"
                        },
                        new
                        {
                            Id = 4,
                            Effect = "You may spend a hero point to call REPEL",
                            Form = "Weapon, One Handed Spear",
                            Materials = "10 Tempest Jade, 10 Ambergelt",
                            Name = "Sydanjaa's Call",
                            Requirement = "Weapon Master"
                        },
                        new
                        {
                            Id = 5,
                            Effect = "You can call SHATTER against an item you hit with both weapons simultaneously by spending a hero point.",
                            Form = "Weapon, Pair, One Handed",
                            Materials = "14 Tempest Jade, 7 Beggars Lye, 12 Dragonbone, 9 Orichalcum",
                            Name = "Bear Claws",
                            Requirement = "Ambidexterity"
                        },
                        new
                        {
                            Id = 6,
                            Effect = "Twice per day you may either call CLEAVE or STRIKEDOWN with this polearm.",
                            Form = "Weapon, Polearm",
                            Materials = "13 Green Iron, 7 Orichalcum, 5 Ambergelt, 1 month.",
                            Name = "Fell Iron Fury",
                            Requirement = "Weapon Master"
                        },
                        new
                        {
                            Id = 7,
                            Effect = "Gain one additional hero point.",
                            Form = "Weapon, Bow/Crossbow",
                            Materials = "9 Green Iron, 5 Ambergelt, 1 Month",
                            Name = "Oathkeeper",
                            Requirement = "Marksman"
                        },
                        new
                        {
                            Id = 8,
                            Effect = "One additional hero point, one additional personal mana",
                            Form = "Weapon, Pair, one handed and wand",
                            Materials = "5 Dragonbone, 6 Green Iron, 5 Iridescent Gloaming, 5 Orichalcum, 3 Tempest Jade, 1 Month",
                            Name = "Trodwalker's Readiness",
                            Requirement = "Ambidexterity, Magician"
                        },
                        new
                        {
                            Id = 9,
                            Effect = "Twice per day you can cast the mend spell as if you know it and without expending any mana.",
                            Form = "Weapon, Wand",
                            Materials = "6 Orichalcum, 3 Iridescent Gloaming, 1 Month",
                            Name = "Redsteel Chisel",
                            Requirement = "Magician"
                        },
                        new
                        {
                            Id = 10,
                            Effect = "When you cast the paralysis spell, you may call IMPALE rather than PARALYSE.",
                            Form = "Weapon, Rod",
                            Materials = "20 Beggar's Lye, 12 Ambergelt, 5 Iridescent Gloaming, 5 Tempest Jade, 1 Month",
                            Name = "Sceptre of the Necropolis",
                            Requirement = "Magician"
                        },
                        new
                        {
                            Id = 11,
                            Effect = "You may cast WEAKNESS as if you know it.",
                            Form = "Weapon, Staff",
                            Materials = "2 Months",
                            Name = "Wendigo's (Draughir's) Bargain",
                            Requirement = "Magician, Battle Mage"
                        },
                        new
                        {
                            Id = 12,
                            Effect = "When you cast, or swift cast, the operate portal spell, or the create bond spell, or perform the discern enchantment, identify ritual performance, identify magical item functions, or discern arcane mark function of detect magic, you may do so without spending any mana.",
                            Form = "Weapon, Ritual Staff",
                            Materials = "15 Iridescent Gloaming, 9 Tempest Jade, 9 Orichalcum, 12 Beggar's Lye, 1 Month",
                            Name = "Staff of the Magi",
                            Requirement = "Magician"
                        },
                        new
                        {
                            Id = 13,
                            Effect = "You may perform ceremonial skills other than dedication as if you were dedicated to the virtue of Vigilance.",
                            Form = "Weapon, Icon",
                            Materials = "5 Iridescent Gloaming, 7 Weltsilver, 9 Dragonbone, 1 Month",
                            Name = "Icon of the Watchful",
                            Requirement = "Dedication"
                        },
                        new
                        {
                            Id = 14,
                            Effect = "You must be dedicated to Wisdom to use this item. Once per day, while you are in an area consecrated to Wisdom, you may spend ten minutes of appropriate roleplaying that includes playing this musical instrument. Any listener who was in the area for the entire period recovers all hero points. You cannot use this ability if you are on a battlefield or in a similar stressful environment. A listener who has lost the ability to recover hero points overnight is not effected by this power.",
                            Form = "Weapon, Instrument",
                            Materials = "7 Ambergelt, 9 Beggar's Lye, 5 Dragonbone",
                            Name = "Goldwood Pipes",
                            Requirement = "Dedication"
                        },
                        new
                        {
                            Id = 15,
                            Effect = "You gain an additional hero point.",
                            Form = "Armour, light Suit",
                            Materials = "9 Green Iron, 5 Iridescent Gloaming, 1 Month",
                            Name = "Runemark Shirt",
                            Requirement = "N/A"
                        },
                        new
                        {
                            Id = 16,
                            Effect = "Gain an additional endurance rank",
                            Form = "Armour, Medium Suit",
                            Materials = "4 Ambergelt, 3 Orichalcum, 1 Month",
                            Name = "Mithril Shirt",
                            Requirement = "N/A"
                        },
                        new
                        {
                            Id = 17,
                            Effect = "Gain an additional 3 ranks of endurance.",
                            Form = "Armour, Heavy Suit",
                            Materials = "14 Orichalcum, 7 Weltsilver, 7 Ambergelt, 3 Beggar's Lye, 1 Month",
                            Name = "Winterborn Warmail",
                            Requirement = "N/A"
                        },
                        new
                        {
                            Id = 18,
                            Effect = "Once per day you can consume a piece of crystal mana to restore three points of spent personal mana.",
                            Form = "Armour, Robe",
                            Materials = "2 Months",
                            Name = "Crystaltender's Vestment",
                            Requirement = "Magician"
                        },
                        new
                        {
                            Id = 19,
                            Effect = "Twice per day when you cast the venom spell, you may do so without spending any mana. You must be able to cast the venom spell to use this power.",
                            Form = "Armour, Mage",
                            Materials = "3 Beggar's Lye, 3 Iridescent Gloaming, 3 Weltsilver, 1 Month",
                            Name = "Wyvernsting Spaulders",
                            Requirement = "Magician, Battle Mage"
                        },
                        new
                        {
                            Id = 20,
                            Effect = "Once per day when you perform or cooperate in the performance of a religious skill you may do so without using a dose of liao.",
                            Form = "Armour, Robe",
                            Materials = "2 Months",
                            Name = "Mendicant Cassock",
                            Requirement = "Dedication"
                        },
                        new
                        {
                            Id = 21,
                            Effect = "You may spend 2 personal mana (instead of 1 hero point) to use the unstoppable skill as if you know it. You must be able to cast spells to use this power - it will not work if you are wearing any armour other than mage armour.",
                            Form = "Talisman, Shield",
                            Materials = "7 Green Iron, 5 Tempest Jade, 4 Dragonbone, 1 Month",
                            Name = "Warcaster's Oath",
                            Requirement = "Shield"
                        },
                        new
                        {
                            Id = 22,
                            Effect = "Twice each day with thirty seconds of appropriate roleplaying you may open a portal as if you had cast the operate portal spell. You may use this power if you are wearing armour.",
                            Form = "Talisman, jewellery",
                            Materials = "6 Weltsilver, 7 Beggar's Lye, 9 Irisescent Gloaming, 1 Month",
                            Name = "Pauper's Key",
                            Requirement = "N/A"
                        },
                        new
                        {
                            Id = 23,
                            Effect = " Once per day you may use this ring to gain one additional rank of any one ritual lore for the purposes of performing a single ritual, subject to the normal rules for effective skill.",
                            Form = "Talisman, Ritual focus",
                            Materials = "7 Orichalcum, 7 Tempest Jade, 7 Weltsilver, 7 Ambergelt, 7 Beggar's Lye, 7 Iridescent Gloaming, 1 Month",
                            Name = "Atun's Ring",
                            Requirement = "Magician"
                        },
                        new
                        {
                            Id = 24,
                            Effect = "Three times per day when you use the physick skill you can draw a little of your own blood and use it as if it were any one herb. An apothecary cannot use this blood when creating a potion.",
                            Form = "Talisman, Tool",
                            Materials = "12 Weltsilver, 12 Ambergelt, 8 Beggar's Lye, 4 Iridescent Gloaming, 1 Month",
                            Name = "Bloodcloak",
                            Requirement = "Physick"
                        },
                        new
                        {
                            Id = 25,
                            Effect = "When you perform or cooperate with the performance of the testimony skill, you may spend up to five additional doses of liao to increase the strength or the ceremony by the same amount.",
                            Form = "Talisman, Ceremonial Regalia",
                            Materials = "5 Tempest Jade, 7 Weltsilver, 7 Ambergelt, 9 Beggar's Lye, 11 Dragonbone, 5 Iridescent Gloaming, 1 Month",
                            Name = "Skein Bowl",
                            Requirement = "Dedication"
                        },
                        new
                        {
                            Id = 26,
                            Effect = "Twice per day when you cast the venom spell, you may do so without spending any mana. You must be able to cast the venom spell to use this power. ",
                            Form = "Armour, Mage",
                            Materials = "3 Beggar's Lye, 3 Iridescent Gloaming, 3 Weltsilver, 1 Month",
                            Name = "Wyvernsting Spaulders",
                            Requirement = "Magician, Battle Mage"
                        },
                        new
                        {
                            Id = 27,
                            Effect = "While wielding this standard you gain five additional personal mana",
                            Form = "Magical Standard",
                            Materials = "7 Orichalcum, 7 Weltsilver, 13 Dragonbone, 11 Beggar's Lye, 12 Iridescent Gloaming, 1 Month",
                            Name = "Celestial Sigil",
                            Requirement = "N/A"
                        },
                        new
                        {
                            Id = 28,
                            Effect = " A banner bonded to this gonfalon may travel to a battle that their national banner is not attending.",
                            Form = "Gonalfon",
                            Materials = "18 Dragonbone, 7 Green Iron, 5 Orichalcum, 9 Iridescent Gloaming, 1 Month",
                            Name = "Mercenary Banner",
                            Requirement = "N/A"
                        },
                        new
                        {
                            Id = 29,
                            Effect = "Twice per day the coven may perform a Spring ritual that does not count towards their daily limit of rituals performed.",
                            Form = "Paraphernalia, ritual",
                            Materials = "12 Ambergelt, 9 Weltsilver, 7 Tempest Jade, 7 Beggar's Lye, 5 Dragonbone, 2 Iridescent Gloaming, 1 Month",
                            Name = "The Fountain of Thorns",
                            Requirement = "N/A"
                        },
                        new
                        {
                            Id = 30,
                            Effect = "The aura of every member of the sect is concealed from the insight ceremony. A character responding to a quick insight must respond \"my aura is concealed\" and provide no other information. ",
                            Form = "Reliquary",
                            Materials = "5 Orichalcum, 7 Tempest Jade, 7 Beggar's Lye, 11 Dragonbone, 1 Month",
                            Name = "Almery of Silence",
                            Requirement = "N/A"
                        });
                });

            modelBuilder.Entity("LRP.Items.Models.Bond", b =>
                {
                    b.HasOne("LRP.Items.Models.Craftable", "Item")
                        .WithMany("Bonds")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
