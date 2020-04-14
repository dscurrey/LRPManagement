using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Models;

namespace Authentication.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string uname, string pword);
        Task<List<User>> GetAll();
    }
}
