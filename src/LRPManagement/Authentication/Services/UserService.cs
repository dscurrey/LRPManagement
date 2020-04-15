using Authentication.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Authentication.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Services
{
    public class UserService : IUserService
    {

        private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "JSmith", Password = "TestPassword1", Role = Role.User, FirstName = "John", LastName = "Smith"},
            new User {Id = 2, Username = "admin", Password = "TestPassword1", Role = Role.Admin, FirstName = "Admin", LastName = "User"},
            new User {Id = 3, Username = "referee", Password = "TestPassword1", Role = Role.Referee, FirstName = "Referee", LastName = "User"}
        };

        private readonly AppSettings _settings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _settings = appSettings.Value;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run( () => _users.FirstOrDefault(u => u.Username == username && u.Password == password));

            if (user == null)
            {
                // User Not found
                return null;
            }

            // JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Secret);
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity
                (
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role) 
                    }
                ),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDesc);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public async Task<List<User>> GetAll()
        {
            return await Task.Run(() => _users.WithoutPasswords());
        }

        public async Task<User> GetById(int id)
        {
            var user = await Task.Run(() => _users.FirstOrDefault(u => u.Id == id));
            return user.WithoutPassword();
        }
    }
}
