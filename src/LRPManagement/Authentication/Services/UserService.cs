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
            new User { Id = 1, FirstName = "John", LastName = "Smith", Username = "JSmith", Password = "TestPassword1"}
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
    }
}
