using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Models;

namespace Authentication.Data
{
    public static class AuthDbInitialiser
    {
        public static async Task SeedDevData(AuthDbContext context, IServiceProvider services)
        {
            if (context.Users.Any())
            {
                // DB Seeded
                return;
            }

            var users = new List<User>
            {
            };

            users.ForEach(u => context.Users.Add(u));
            await context.SaveChangesAsync();
        }
    }
}
