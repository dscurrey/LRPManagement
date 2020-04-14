using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    public class UsersController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok("Hello World");
        }
    }
}