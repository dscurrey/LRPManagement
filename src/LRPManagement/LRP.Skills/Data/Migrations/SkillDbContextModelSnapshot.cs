﻿// <auto-generated />
using LRP.Skills.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LRP.Skills.Data.Migrations
{
    [DbContext(typeof(SkillDbContext))]
    partial class SkillDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LRP.Skills.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("XpCost")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Skill");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Thrown",
                            XpCost = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Ambidexterity",
                            XpCost = 1
                        },
                        new
                        {
                            Id = 3,
                            Name = "Weapon Master",
                            XpCost = 2
                        },
                        new
                        {
                            Id = 4,
                            Name = "Marksman",
                            XpCost = 4
                        },
                        new
                        {
                            Id = 5,
                            Name = "Shield",
                            XpCost = 2
                        },
                        new
                        {
                            Id = 6,
                            Name = "Endurance",
                            XpCost = 2
                        },
                        new
                        {
                            Id = 7,
                            Name = "Fortitude",
                            XpCost = 1
                        },
                        new
                        {
                            Id = 8,
                            Name = "Hero",
                            XpCost = 2
                        },
                        new
                        {
                            Id = 9,
                            Name = "Cleaving Strike",
                            XpCost = 1
                        },
                        new
                        {
                            Id = 10,
                            Name = "Mortal Blow",
                            XpCost = 1
                        },
                        new
                        {
                            Id = 11,
                            Name = "Mighty Strikedown",
                            XpCost = 1
                        },
                        new
                        {
                            Id = 12,
                            Name = "Relentless",
                            XpCost = 2
                        },
                        new
                        {
                            Id = 13,
                            Name = "Unstoppable",
                            XpCost = 2
                        },
                        new
                        {
                            Id = 14,
                            Name = "Stay With Me",
                            XpCost = 1
                        },
                        new
                        {
                            Id = 15,
                            Name = "Get it Together",
                            XpCost = 1
                        },
                        new
                        {
                            Id = 16,
                            Name = "Chirurgeon",
                            XpCost = 1
                        },
                        new
                        {
                            Id = 17,
                            Name = "Physick",
                            XpCost = 3
                        },
                        new
                        {
                            Id = 18,
                            Name = "Apothecary",
                            XpCost = 2
                        },
                        new
                        {
                            Id = 19,
                            Name = "Artisan",
                            XpCost = 4
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
