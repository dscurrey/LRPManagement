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
    [Migration("20200316175156_ExtraDemoData")]
    partial class ExtraDemoData
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

                    b.HasKey("Id");

                    b.ToTable("Character");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = true,
                            IsRetired = false,
                            Name = "Test user's 1st Character'",
                            PlayerId = 1
                        },
                        new
                        {
                            Id = 2,
                            IsActive = true,
                            IsRetired = false,
                            Name = "Test user 2's 1st Character'",
                            PlayerId = 2
                        });
                });
#pragma warning restore 612, 618
        }
    }
}