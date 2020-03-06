using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRP.Skills.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace LRP.Skills.Data
{
    public class SkillDbContext : DbContext
    {
        public virtual DbSet<Skill> Skill { get; set; }
        private IWebHostEnvironment HostEnv { get; set; }

        public SkillDbContext(DbContextOptions options, IWebHostEnvironment hostEnv) : base(options)
        {
            HostEnv = hostEnv;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            if (HostEnv != null && HostEnv.IsDevelopment())
            {
                // Seed Data (Dev)
                builder.Entity<Skill>().HasData(
                    new Skill { Id = 1, Name = "Weapon Master"},
                    new Skill { Id = 2, Name = "Artisan"}
                );
            }
        }
    }
}
