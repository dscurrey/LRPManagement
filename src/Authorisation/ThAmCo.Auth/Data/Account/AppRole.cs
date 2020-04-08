using Microsoft.AspNetCore.Identity;

namespace LRP.Auth.Data.Account
{
    public class AppRole : IdentityRole
    {
        public string Descriptor { get; set; }
    }
}
