﻿using LRP.Players.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;

namespace LRP.Players.Data
{
    public class PlayerDbContext : DbContext
    {
        public virtual DbSet<Player> Player { get; set; }

        private IWebHostEnvironment HostEnv { get; }

        public PlayerDbContext(DbContextOptions<PlayerDbContext> options, IWebHostEnvironment env) : base(options)
        {
            HostEnv = env;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            if (HostEnv != null && HostEnv.IsDevelopment())
                // Seed Data
                builder.Entity<Player>().HasData
                (
                    new Player
                    {
                        Id = 1, FirstName = "Test", LastName = "user", IsActive = true, DateJoined = DateTime.Now, AccountRef = "test@dcurrey.co.uk"
                    },
                    new Player
                    {
                        Id = 2,
                        FirstName = "John",
                        LastName = "Smith",
                        IsActive = true,
                        DateJoined = DateTime.Now,
                        AccountRef = "jsmith@dcurrey.co.uk"
                    },
                    new Player
                    {
                        Id = 3,
                        FirstName = "Valdemar",
                        LastName = "Karlsson",
                        IsActive = true,
                        DateJoined = DateTime.Now,
                        AccountRef = "val@dcurrey.co.uk"
                    }
                );
        }
    }
}