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
                new User
                {
                    Username = "JSmith", Password = "TestPassword1", Role = Role.User, FirstName = "John",
                    LastName = "Smith"
                },
                new User
                {
                    Username = "admin", Password = "TestPassword1", Role = Role.Admin, FirstName = "Admin",
                    LastName = "User"
                },
                new User
                {
                    Username = "referee", Password = "TestPassword1", Role = Role.Referee,
                    FirstName = "Referee", LastName = "User"
                }
            };

            users.ForEach(u => context.Users.Add(u));
            await context.SaveChangesAsync();
        }
    }
}
