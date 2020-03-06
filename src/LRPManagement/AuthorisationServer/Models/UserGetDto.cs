using System.Collections.Generic;

namespace AuthorisationServer.Models
{
    public class UserGetDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
