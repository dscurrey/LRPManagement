﻿// <auto-generated />
using LRP.Characters.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LRP.Characters.Data.Migrations
{
    [DbContext(typeof(CharacterDbContext))]
    [Migration("20200506125622_ImprovedTestData")]
    partial class ImprovedTestData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LRP.Characters.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRetired")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Xp")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Character");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = false,
                            IsRetired = true,
                            Name = "Player 1, Retired",
                            PlayerId = 1,
                            Xp = 4
                        },
                        new
                        {
                            Id = 2,
                            IsActive = true,
                            IsRetired = false,
                            Name = "Player 2, Active",
                            PlayerId = 2,
                            Xp = 8
                        },
                        new
                        {
                            Id = 3,
                            IsActive = true,
                            IsRetired = false,
                            Name = "Player 1, Active",
                            PlayerId = 1,
                            Xp = 8
                        },
                        new
                        {
                            Id = 4,
                            IsActive = true,
                            IsRetired = false,
                            Name = "Player 1, Inactive",
                            PlayerId = 1,
                            Xp = 8
                        });
                });
#pragma warning restore 612, 618
        }
    }
}