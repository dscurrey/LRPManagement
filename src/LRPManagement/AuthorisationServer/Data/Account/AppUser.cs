using Microsoft.AspNetCore.Identity;

namespace AuthorisationServer.Data.Account
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
