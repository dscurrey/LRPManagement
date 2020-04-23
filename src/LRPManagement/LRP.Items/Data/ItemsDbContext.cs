using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRP.Items.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace LRP.Items.Data
{
    public class ItemsDbContext : DbContext
    {
        public virtual DbSet<Craftable> Craftables { get; set; }
        private IWebHostEnvironment HostEnv { get; }

        public ItemsDbContext(DbContextOptions options, IWebHostEnvironment hostEnv) : base(options)
        {
            HostEnv = hostEnv;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            if (HostEnv != null && HostEnv.IsDevelopment())
            {
                // Seed Data (Dev)
                builder.Entity<Craftable>().HasData
                (
                    new Craftable
                    {
                        Id = 1,
                        Name = "Apprentice's Blade",
                        Form = "Weapon, One Handed",
                        Requirement = "N/A",
                        Effect = "Spend one hero point to call CLEAVE",
                        Materials = "N/A"
                    }
                );
            }
        }
    }
}
