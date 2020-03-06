using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace AuthorisationServer.Data.Account
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
                new AppUser { UserName = "Referee1", Email = "ref1@example.com", FullName = "Sample Referee 1" },
                new AppUser { UserName = "Referee2", Email = "ref2@example.com", FullName = "Sample Referee 2" },
                new AppUser { UserName = "Staff1", Email = "staff1@example.com", FullName = "Sample Staff 1" },
                new AppUser { UserName = "Staff2", Email = "staff2@example.com", FullName = "Sample Staff 2" },
                new AppUser { UserName = "Player1", Email = "play1@example.com", FullName = "Sample Player 1" },
                new AppUser { UserName = "Player2", Email = "play2@example.com", FullName = "Sample Player 2" },
            };
            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Password1_");
                // auto confirm email addresses for test users
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                await userManager.ConfirmEmailAsync(user, token);
            }

            // Add to roles
            await userManager.AddToRoleAsync(users[0], "Referee");
            await userManager.AddToRoleAsync(users[1], "Referee");
            await userManager.AddToRoleAsync(users[2], "Staff");
            await userManager.AddToRoleAsync(users[3], "Staff");
            await userManager.AddToRoleAsync(users[4], "Player");
            await userManager.AddToRoleAsync(users[5], "Player");
        }
    }
}
