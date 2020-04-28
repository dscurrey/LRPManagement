using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace LRPManagement.Services
{
    public class TokenBuilder : ITokenBuilder
    {
        private SignInManager<IdentityUser> _signInManager;
        private IHttpContextAccessor _httpContext;

        public TokenBuilder(IHttpContextAccessor context,
            SignInManager<IdentityUser> signInManager)
        {
            _httpContext = context;
            _signInManager = signInManager;
        }

        public async Task<string> BuildToken(string username)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("placeholder-key-that-is-long-enough-for-sha256"));
            var signingCreds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var user = await _signInManager.UserManager.Users.Where
                (u => u.UserName == username).FirstOrDefaultAsync();
            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(ClaimTypes.Role, userRoles.FirstOrDefault()), 
            };
            var jwt = new JwtSecurityToken(claims: claims, signingCredentials: signingCreds);
            var encJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encJwt;
        }

        public async Task<string> BuildToken()
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("placeholder-key-that-is-long-enough-for-sha256"));
            var signingCreds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var username = _httpContext.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            var user = await _signInManager.UserManager.Users.Where
                (u => u.UserName == username).FirstOrDefaultAsync();
            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(ClaimTypes.Role, userRoles.FirstOrDefault()),
            };
            var jwt = new JwtSecurityToken(claims: claims, signingCredentials: signingCreds);
            var encJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encJwt;
        }
    }
}
