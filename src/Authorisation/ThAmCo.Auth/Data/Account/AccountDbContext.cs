using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LRP.Auth.Data.Account
{
    public class AccountDbContext : IdentityDbContext<AppUser>
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("account");

            builder.Entity<AppRole>().HasData(
                new AppRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Descriptor = "LRP Administrators"
                },
                new AppRole
                {
                    Name = "Referee",
                    NormalizedName = "REFEREE",
                    Descriptor = "Referee Staff Members"
                },
                new AppRole
                {
                    Name = "Player",
                    NormalizedName = "PLAYER",
                    Descriptor = "LRP Players"
                }
            );
        }
    }
}
