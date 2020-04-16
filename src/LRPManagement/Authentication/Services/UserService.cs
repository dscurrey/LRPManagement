using Authentication.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Authentication.Data;
using Authentication.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _settings;
        private AuthDbContext _context;

        public UserService(IOptions<AppSettings> appSettings, AuthDbContext context)
        {
            _settings = appSettings.Value;
            _context = context;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

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
            var users = await _context.Users.ToListAsync();
            return users.WithoutPasswords();
        }

        public async Task<User> GetById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user.WithoutPassword();
        }

        public async Task<User> Create(User user, string password, string role)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new AuthException("Password is Required");
            }

            if (_context.Users.Any(u => u.Username == user.Username))
            {
                throw new AuthException("Username \""+ user.Username +"\" already exists");
            }

            // TODO - Password hashing

            if (role.Equals(Role.Admin))
            {
                user.Role = Role.Admin;
            }
            else if (role.Equals(Role.Referee))
            {
                user.Role = Role.Referee;
            }
            else
            {
                user.Role = Role.User;
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        private static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password cannot be empty");
            }

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password cannot be empty");
            }

            if (hash.Length != 64 || salt.Length != 128)
            {
                throw new ArgumentException("Invalid length of hash/salt");
            }

            using var hmac = new System.Security.Cryptography.HMACSHA512(salt);
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != hash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
