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

        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "John", LastName = "Smith", Username = "JSmith", Password = "TestPassword1"}
        };

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run( () => _users.FirstOrDefault(u => u.Username == username && u.Password == password));

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
