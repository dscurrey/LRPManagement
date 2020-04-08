using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LRP.Auth.Data.Account
{
    static public class AccountDbInitialiser
    {
        public static async Task SeedTestData(AccountDbContext context,
                                              IServiceProvider services)
        {
            // exit early if any existing data is present
            if (context.Users.Any())
            {
                return;
            }

            var userManager = services.GetRequiredService<UserManager<AppUser>>();

            AppUser[] users = {
                new AppUser { UserName = "admin@example.com", Email = "admin@example.com", FullName = "Example Admin/Staff User" },
                new AppUser { UserName = "ref@example.com", Email = "ref@example.com", FullName = "Example Referee User" },
                new AppUser { UserName = "player@example.com", Email = "player@example.com", FullName = "Example Player" }
            };
            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Password1_");
                // auto confirm email addresses for test users
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                await userManager.ConfirmEmailAsync(user, token);
            }

            await userManager.AddToRoleAsync(users[0], "Admin");
            await userManager.AddToRoleAsync(users[1], "Referee");
            await userManager.AddToRoleAsync(users[2], "Player");
        }
    }
}
