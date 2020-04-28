using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using LRPManagement.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LRPManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenBuilder _tokenBuilder;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticationController(ITokenBuilder tokenBuilder, UserManager<IdentityUser> userManager)
        {
            _tokenBuilder = tokenBuilder;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userDto.Username);
            if (user == null)
            {
                return NotFound();
            }

            if (!await UserIsValid(userDto))
            {
                return BadRequest();
            }

            var token = _tokenBuilder.BuildToken(userDto.Username);

            return Ok(token);
        }

        [HttpGet("verify")]
        public async Task<IActionResult> VerifyToken()
        {
            var username = User.Claims.SingleOrDefault();
            if (username == null)
            {
                return Unauthorized();
            }

            var userExists = await _userManager.Users.AnyAsync(u => u.UserName == username.Value);
            if (!userExists)
            {
                return Unauthorized();
            }

            return NoContent();
        }

        private async Task<bool> UserIsValid(UserDto userDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userDto.Username);
            return await _userManager.CheckPasswordAsync(user, userDto.Password);
        }
    }
}