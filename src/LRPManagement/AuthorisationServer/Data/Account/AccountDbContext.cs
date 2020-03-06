using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthorisationServer.Data.Account
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
                    Name = "Referee",
                    NormalizedName = "REFEREE",
                    Descriptor = "Game Referee"
                },
                new AppRole
                {
                    Name = "Staff",
                    NormalizedName = "STAFF",
                    Descriptor = "Game Staff (Game Operations Desk)"
                },
                new AppRole
                {
                    Name = "Player",
                    NormalizedName = "PLAYER",
                    Descriptor = "Game Player"
                }
            );
        }
    }
}
