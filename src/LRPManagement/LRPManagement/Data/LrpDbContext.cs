using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRPManagement.Models;

namespace LRPManagement.Data
{
    public class LrpDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<CharSkill> CharacterSkills { get; set; }
        public DbSet<Player> Players { get; set; }
        //public DbSet<PreReqSkill> PreReqSkills { get; set; }

        public LrpDbContext(DbContextOptions<LrpDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add composite keys
            //modelBuilder.Entity<PreReqSkill>()
            //    .HasKey(s => new {s.SkillId, s.PreReqId});

            //modelBuilder.Entity<PreReqSkill>()
            //    .HasOne(s => s.Skill)
            //    .WithOne(p => p.PreReqSkill)
            //    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CharSkill>()
                .HasKey(c => new { c.CharId, c.SkillId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
