using System.Collections.Generic;

namespace AuthorisationServer.Models
{
    public class UserPutDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
