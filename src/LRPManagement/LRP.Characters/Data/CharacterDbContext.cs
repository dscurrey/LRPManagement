using LRP.Characters.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
                // Seed Data (Dev)
                builder.Entity<Character>().HasData
                (
                    new Character
                    {
                        Id = 1,
                        Name = "Player 1, Retired",
                        IsActive = false,
                        IsRetired = true,
                        PlayerId = 1,
                        Xp = 4
                    },
                    new Character
                    {
                        Id = 2,
                        Name = "Player 2, Active",
                        IsActive = true,
                        IsRetired = false,
                        PlayerId = 2,
                        Xp = 8
                    },
                    new Character
                    {
                        Id = 3,
                        Name = "Player 1, Active",
                        IsActive = true,
                        IsRetired = false,
                        PlayerId = 1,
                        Xp = 8
                    },
                    new Character
                    {
                        Id = 4,
                        Name = "Player 1, Inactive",
                        IsActive = true,
                        IsRetired = false,
                        PlayerId = 1,
                        Xp = 8
                    }
                );
        }
    }
}