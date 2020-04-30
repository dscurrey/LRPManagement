using LRP.Items.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace LRP.Items.Data
{
    public class ItemsDbContext : DbContext
    {
        public virtual DbSet<Craftable> Craftables { get; set; }
        public virtual DbSet<Bond> Bonds { get; set; }

        private IWebHostEnvironment HostEnv { get; }

        public ItemsDbContext(DbContextOptions options, IWebHostEnvironment hostEnv) : base(options)
        {
            HostEnv = hostEnv;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Craftable>()
                .HasMany(c => c.Bonds)
                .WithOne(b => b.Item);

            if (HostEnv != null && HostEnv.IsDevelopment())
            {
                // Seed Data (Dev)
                builder.Entity<Craftable>().HasData
                (
                    new Craftable
                    {
                        Id = 1,
                        Name = "Apprentice's Blade",
                        Form = "Weapon, One Handed",
                        Requirement = "N/A",
                        Effect = "Spend one hero point to call CLEAVE",
                        Materials = "N/A"
                    },
                    new Craftable
                    {
                        Id = 2,
                        Name = "Scorpion's Sting",
                        Form = "Weapon",
                        Requirement = "N/A",
                        Effect = "Once per day, call VENOM. You are also affected.",
                        Materials = "7 Beggar's Lye, 5 Ambergelt, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 3,
                        Name = "Landsknecht's Zweihänder",
                        Form = "Weapon, Two Handed",
                        Requirement = "Weapon Master",
                        Effect = "Twice per day you may call IMPALE",
                        Materials = "13 Tempest Jade, 3 Orichalcum, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 4,
                        Name = "Sydanjaa's Call",
                        Form = "Weapon, One Handed Spear",
                        Requirement = "Weapon Master",
                        Effect = "You may spend a hero point to call REPEL",
                        Materials = "10 Tempest Jade, 10 Ambergelt"
                    },
                    new Craftable
                    {
                        Id = 5,
                        Name = "Bear Claws",
                        Form = "Weapon, Pair, One Handed",
                        Requirement = "Ambidexterity",
                        Effect = "You can call SHATTER against an item you hit with both weapons simultaneously by spending a hero point.",
                        Materials = "14 Tempest Jade, 7 Beggars Lye, 12 Dragonbone, 9 Orichalcum"
                    },
                    new Craftable
                    {
                        Id = 6,
                        Name = "Fell Iron Fury",
                        Form = "Weapon, Polearm",
                        Requirement = "Weapon Master",
                        Effect = "Twice per day you may either call CLEAVE or STRIKEDOWN with this polearm.",
                        Materials = "13 Green Iron, 7 Orichalcum, 5 Ambergelt, 1 month."
                    },
                    new Craftable
                    {
                        Id = 7,
                        Name = "Oathkeeper",
                        Form = "Weapon, Bow/Crossbow",
                        Requirement = "Marksman",
                        Effect = "Gain one additional hero point.",
                        Materials = "9 Green Iron, 5 Ambergelt, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 8,
                        Name = "Trodwalker's Readiness",
                        Form = "Weapon, Pair, one handed and wand",
                        Requirement = "Ambidexterity, Magician",
                        Effect = "One additional hero point, one additional personal mana",
                        Materials = "5 Dragonbone, 6 Green Iron, 5 Iridescent Gloaming, 5 Orichalcum, 3 Tempest Jade, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 9,
                        Name = "Redsteel Chisel",
                        Form = "Weapon, Wand",
                        Requirement = "Magician",
                        Effect = "Twice per day you can cast the mend spell as if you know it and without expending any mana.",
                        Materials = "6 Orichalcum, 3 Iridescent Gloaming, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 10,
                        Name = "Sceptre of the Necropolis",
                        Form = "Weapon, Rod",
                        Requirement = "Magician",
                        Effect = "When you cast the paralysis spell, you may call IMPALE rather than PARALYSE.",
                        Materials = "20 Beggar's Lye, 12 Ambergelt, 5 Iridescent Gloaming, 5 Tempest Jade, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 11,
                        Name = "Wendigo's (Draughir's) Bargain",
                        Form = "Weapon, Staff",
                        Requirement = "Magician, Battle Mage",
                        Effect = "You may cast WEAKNESS as if you know it.",
                        Materials = "2 Months"
                    },
                    new Craftable
                    {
                        Id = 12,
                        Name = "Staff of the Magi",
                        Form = "Weapon, Ritual Staff",
                        Requirement = "Magician",
                        Effect = "When you cast, or swift cast, the operate portal spell, or the create bond spell, or perform the discern enchantment, identify ritual performance, identify magical item functions, or discern arcane mark function of detect magic, you may do so without spending any mana.",
                        Materials = "15 Iridescent Gloaming, 9 Tempest Jade, 9 Orichalcum, 12 Beggar's Lye, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 13,
                        Name = "Icon of the Watchful",
                        Form = "Weapon, Icon",
                        Requirement = "Dedication",
                        Effect = "You may perform ceremonial skills other than dedication as if you were dedicated to the virtue of Vigilance.",
                        Materials = "5 Iridescent Gloaming, 7 Weltsilver, 9 Dragonbone, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 14,
                        Name = "Goldwood Pipes",
                        Form = "Weapon, Instrument",
                        Requirement = "Dedication",
                        Effect = "You must be dedicated to Wisdom to use this item. Once per day, while you are in an area consecrated to Wisdom, you may spend ten minutes of appropriate roleplaying that includes playing this musical instrument. Any listener who was in the area for the entire period recovers all hero points. You cannot use this ability if you are on a battlefield or in a similar stressful environment. A listener who has lost the ability to recover hero points overnight is not effected by this power.",
                        Materials = "7 Ambergelt, 9 Beggar's Lye, 5 Dragonbone"
                    },
                    new Craftable
                    {
                        Id = 15,
                        Name = "Runemark Shirt",
                        Form = "Armour, light Suit",
                        Requirement = "N/A",
                        Effect = "You gain an additional hero point.",
                        Materials = "9 Green Iron, 5 Iridescent Gloaming, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 16,
                        Name = "Mithril Shirt",
                        Form = "Armour, Medium Suit",
                        Requirement = "N/A",
                        Effect = "Gain an additional endurance rank",
                        Materials = "4 Ambergelt, 3 Orichalcum, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 17,
                        Name = "Winterborn Warmail",
                        Form = "Armour, Heavy Suit",
                        Requirement = "N/A",
                        Effect = "Gain an additional 3 ranks of endurance.",
                        Materials = "14 Orichalcum, 7 Weltsilver, 7 Ambergelt, 3 Beggar's Lye, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 18,
                        Name = "Crystaltender's Vestment",
                        Form = "Armour, Robe",
                        Requirement = "Magician",
                        Effect = "Once per day you can consume a piece of crystal mana to restore three points of spent personal mana.",
                        Materials = "2 Months"
                    },
                    new Craftable
                    {
                        Id = 19,
                        Name = "Wyvernsting Spaulders",
                        Form = "Armour, Mage",
                        Requirement = "Magician, Battle Mage",
                        Effect = "Twice per day when you cast the venom spell, you may do so without spending any mana. You must be able to cast the venom spell to use this power.",
                        Materials = "3 Beggar's Lye, 3 Iridescent Gloaming, 3 Weltsilver, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 20,
                        Name = "Mendicant Cassock",
                        Form = "Armour, Robe",
                        Requirement = "Dedication",
                        Effect = "Once per day when you perform or cooperate in the performance of a religious skill you may do so without using a dose of liao.",
                        Materials = "2 Months"
                    },
                    new Craftable
                    {
                        Id = 21,
                        Name = "Warcaster's Oath",
                        Form = "Talisman, Shield",
                        Requirement = "Shield",
                        Effect = "You may spend 2 personal mana (instead of 1 hero point) to use the unstoppable skill as if you know it. You must be able to cast spells to use this power - it will not work if you are wearing any armour other than mage armour.",
                        Materials = "7 Green Iron, 5 Tempest Jade, 4 Dragonbone, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 22,
                        Name = "Pauper's Key",
                        Form = "Talisman, jewellery",
                        Requirement = "N/A",
                        Effect = "Twice each day with thirty seconds of appropriate roleplaying you may open a portal as if you had cast the operate portal spell. You may use this power if you are wearing armour.",
                        Materials = "6 Weltsilver, 7 Beggar's Lye, 9 Irisescent Gloaming, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 23,
                        Name = "Atun's Ring",
                        Form = "Talisman, Ritual focus",
                        Requirement = "Magician",
                        Effect = " Once per day you may use this ring to gain one additional rank of any one ritual lore for the purposes of performing a single ritual, subject to the normal rules for effective skill.",
                        Materials = "7 Orichalcum, 7 Tempest Jade, 7 Weltsilver, 7 Ambergelt, 7 Beggar's Lye, 7 Iridescent Gloaming, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 24,
                        Name = "Bloodcloak",
                        Form = "Talisman, Tool",
                        Requirement = "Physick",
                        Effect = "Three times per day when you use the physick skill you can draw a little of your own blood and use it as if it were any one herb. An apothecary cannot use this blood when creating a potion.",
                        Materials = "12 Weltsilver, 12 Ambergelt, 8 Beggar's Lye, 4 Iridescent Gloaming, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 25,
                        Name = "Skein Bowl",
                        Form = "Talisman, Ceremonial Regalia",
                        Requirement = "Dedication",
                        Effect = "When you perform or cooperate with the performance of the testimony skill, you may spend up to five additional doses of liao to increase the strength or the ceremony by the same amount.",
                        Materials = "5 Tempest Jade, 7 Weltsilver, 7 Ambergelt, 9 Beggar's Lye, 11 Dragonbone, 5 Iridescent Gloaming, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 26,
                        Name = "Fireglass",
                        Form = "Talisaman, Ceremonial Regalia",
                        Requirement = "Dedication",
                        Effect = "Three times per day when you use the anointing skill you may restore a spent hero point to your target rather than creating a personal aura.",
                        Materials = "3 Beggar's Lye, 3 Iridescent Gloaming, 3 Weltsilver, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 27,
                        Name = "Celestial Sigil",
                        Form = "Magical Standard",
                        Requirement = "N/A",
                        Effect = "While wielding this standard you gain five additional personal mana",
                        Materials = "9 Tempest Jade, 7 Orichalcum, 5 Green Iron, 10 Dragonbone, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 28,
                        Name = "Mercenary Banner",
                        Form = "Gonalfon",
                        Requirement = "N/A",
                        Effect = " A banner bonded to this gonfalon may travel to a battle that their national banner is not attending.",
                        Materials = "18 Dragonbone, 7 Green Iron, 5 Orichalcum, 9 Iridescent Gloaming, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 29,
                        Name = "The Fountain of Thorns",
                        Form = "Paraphernalia, ritual",
                        Requirement = "N/A",
                        Effect = "Twice per day the coven may perform a Spring ritual that does not count towards their daily limit of rituals performed.",
                        Materials = "12 Ambergelt, 9 Weltsilver, 7 Tempest Jade, 7 Beggar's Lye, 5 Dragonbone, 2 Iridescent Gloaming, 1 Month"
                    },
                    new Craftable
                    {
                        Id = 30,
                        Name = "Almery of Silence",
                        Form = "Reliquary",
                        Requirement = "N/A",
                        Effect = "The aura of every member of the sect is concealed from the insight ceremony. A character responding to a quick insight must respond \"my aura is concealed\" and provide no other information. ",
                        Materials = "5 Orichalcum, 7 Tempest Jade, 7 Beggar's Lye, 11 Dragonbone, 1 Month"
                    }
                );
            }
        }
    }
}
