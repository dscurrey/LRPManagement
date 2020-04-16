﻿using Authentication.Models;
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

        public async Task<User> Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                // TODO - Handle empty password
            }

            if (_context.Users.Any(u => u.Username == user.Username))
            {
                // TODO - Handle Username Exists
            }

            // TODO - Password hashing

            user.Role = Role.User;

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
    }
}
