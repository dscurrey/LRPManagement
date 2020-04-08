using Microsoft.AspNetCore.Identity;

namespace LRP.Auth.Data.Account
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
