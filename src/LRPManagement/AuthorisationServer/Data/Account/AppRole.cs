using Microsoft.AspNetCore.Identity;

namespace AuthorisationServer.Data.Account
{
    public class AppRole : IdentityRole
    {
        public string Descriptor { get; set; }
    }
}
