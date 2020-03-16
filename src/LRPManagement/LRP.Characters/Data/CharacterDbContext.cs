using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRP.Characters.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace LRP.Characters.Data
{
    public class CharacterDbContext : DbContext
    {
        public virtual DbSet<Character> Character { get; set; }

        private IWebHostEnvironment HostEnv { get; }

        public CharacterDbContext(DbContextOptions options, IWebHostEnvironment hostEnv) : base(options)
        {
            HostEnv = hostEnv;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            if (HostEnv != null && HostEnv.IsDevelopment())
            {
                // Seed Data (Dev)
                builder.Entity<Character>().HasData
                (
                    new Character {Id = 1, Name = "Test user's 1st Character'", IsActive = true, IsRetired = false, PlayerId = 1},
                    new Character {Id = 2, Name = "Test user 2's 1st Character'", IsActive = true, IsRetired = false, PlayerId = 2 }
                );
            }
        }
    }
}
