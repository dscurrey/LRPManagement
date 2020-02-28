using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRPManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LRPManagement.Data
{
    public static class DbInitialiser
    {
        public static void Initialise(LrpDbContext context)
        {
            context.Database.EnsureCreated();
            //context.Database.Migrate();

            if (context.Skills.Any())
            {
                return; // DB Contains skills (is seeded)
            }

            var skills = new Skill[]
            {
                new Skill {Name = "Test Skill 1"},
                new Skill {Name = "Test Skill 2"}
            };
            foreach (Skill s in skills)
            {
                context.Skills.Add(s);
            }
            context.SaveChanges();

            // TODO - Seed Players
            // TODO - Seed Characters
        }
    }
}
