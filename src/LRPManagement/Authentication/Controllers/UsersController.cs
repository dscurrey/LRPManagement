using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Models;
using Authentication.Services;
using Authentication.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet("test")]
        public async Task<IActionResult> TestAuth()
        {
            if (User.IsInRole(Role.User))
            {
                return Ok(Role.User);
            }

            if (User.IsInRole(Role.Referee))
            {
                return Ok(Role.Referee);
            }

            if (User.IsInRole(Role.Admin))
            {
                return Ok(Role.Admin);
            }

            return Ok("Unregistered");
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return Ok("Hello World");
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
            {
                return BadRequest(new {message = "Username or password is incorrect."});
            }

            return Ok(user);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var curId = int.Parse(User.Identity.Name);
            if (id != curId && !User.IsInRole(Role.Admin))
            {
                // User is looking for a user other than itself
                // And is not admin
                return Forbid();
            }

            var user = await _userService.GetById(id);

            if (user == null)
            {
                return NotFound(new { message = "User with ID: "+ id + "not found." });
            }

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterDTO model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Username = model.UserName
            };

            try
            {
                await Task.Run( () => _userService.Create(user, user.Password));
                return Ok(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}