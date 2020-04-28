using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace LRPManagement.Services
{
    public class TokenBuilder : ITokenBuilder
    {
        public string BuildToken(string username)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("placeholder-key-that-is-long-enough-for-sha256"));
            var signingCreds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username)
            };
            var jwt = new JwtSecurityToken(claims: claims, signingCredentials: signingCreds);
            var encJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encJwt;
        }
    }
}
