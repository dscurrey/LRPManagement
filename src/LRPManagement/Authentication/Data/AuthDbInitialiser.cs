using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Models;
using Authentication.Services;

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

            var user = new User {FirstName = "Admin", LastName = "User", Username = "admin", Role = Role.Admin};
            byte[] hash, salt;
            UserService.CreatePasswordHash("TestPassword1", out hash, out salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            context.Users.Add(user);
            await context.SaveChangesAsync();
        }
    }
}
