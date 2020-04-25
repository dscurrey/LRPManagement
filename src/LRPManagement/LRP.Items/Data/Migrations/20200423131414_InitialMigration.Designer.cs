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
    [Migration("20200423131414_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                            Requirement = "N/A"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}