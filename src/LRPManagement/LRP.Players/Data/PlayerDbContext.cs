using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRP.Players.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Hosting;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            if (HostEnv != null && HostEnv.IsDevelopment())
            {
                // Seed Data
                builder.Entity<Player>().HasData(
                    new Player { Id = 1, FirstName = "Test", LastName = "user", IsActive = true, DateJoined = DateTime.Now}
                );
            }
        }
    }
}
