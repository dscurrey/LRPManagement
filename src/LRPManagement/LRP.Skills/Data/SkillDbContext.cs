using LRP.Skills.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace LRP.Skills.Data
{
    public class SkillDbContext : DbContext
    {
        public virtual DbSet<Skill> Skill { get; set; }
        public virtual DbSet<CharacterSkill> CharacterSkills { get; set; }
        private IWebHostEnvironment HostEnv { get; set; }

        public SkillDbContext(DbContextOptions options, IWebHostEnvironment hostEnv) : base(options)
        {
            HostEnv = hostEnv;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Skill>()
                .HasMany(s => s.CharacterSkills)
                .WithOne(cs => cs.Skill);

            if (HostEnv != null && HostEnv.IsDevelopment())
                // Seed Data (Dev)
                builder.Entity<Skill>().HasData
                (
                    new Skill {Id = 1, Name = "Thrown", XpCost = 1},
                    new Skill {Id = 2, Name = "Ambidexterity", XpCost = 1},
                    new Skill {Id = 3, Name = "Weapon Master", XpCost = 2},
                    new Skill {Id = 4, Name = "Marksman", XpCost = 4},
                    new Skill {Id = 5, Name = "Shield", XpCost = 2},
                    new Skill {Id = 6, Name = "Endurance", XpCost = 2},
                    new Skill {Id = 7, Name = "Fortitude", XpCost = 1},
                    new Skill {Id = 8, Name = "Hero", XpCost = 2},
                    new Skill {Id = 9, Name = "Cleaving Strike", XpCost = 1},
                    new Skill {Id = 10, Name = "Mortal Blow", XpCost = 1},
                    new Skill {Id = 11, Name = "Mighty Strikedown", XpCost = 1},
                    new Skill {Id = 12, Name = "Relentless", XpCost = 2},
                    new Skill {Id = 13, Name = "Unstoppable", XpCost = 2},
                    new Skill {Id = 14, Name = "Stay With Me", XpCost = 1},
                    new Skill {Id = 15, Name = "Get it Together", XpCost = 1},
                    new Skill {Id = 16, Name = "Chirurgeon", XpCost = 1},
                    new Skill {Id = 17, Name = "Physick", XpCost = 3},
                    new Skill {Id = 18, Name = "Apothecary", XpCost = 2},
                    new Skill {Id = 19, Name = "Artisan", XpCost = 4}
                );
        }
    }
}