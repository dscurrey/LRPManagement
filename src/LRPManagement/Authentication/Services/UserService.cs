using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Helpers;

namespace Authentication.Services
{
    public class UserService : IUserService
    {

        private List<User> _users = new List<User>();

        public async Task<User> Authenticate(string uname, string pword)
        {
            var user = await Task.Run( () => _users.FirstOrDefault(u => u.Username == uname && u.Password == pword));

            if (user == null)
            {
                // User Not found
                return null;
            }

            return user.WithoutPassword();
        }

        public async Task<List<User>> GetAll()
        {
            return await Task.Run(() => _users.WithoutPasswords());
        }
    }
}
