using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Models;

namespace Authentication.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> Create(User user, string password);
        void Update(User user);
        void Delete();
    }
}
