using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LRPManagement.Data
{
    public static class DbInitialiser
    {
        public static void Initialise(LrpDbContext context)
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();

            if (context.Skills.Any()) return; // DB Contains skills (is seeded)
        }
    }
}