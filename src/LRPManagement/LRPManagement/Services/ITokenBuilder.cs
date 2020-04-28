using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Services
{
    public interface ITokenBuilder
    {
        Task<string> BuildToken(string username);
        Task<string> BuildToken();
    }
}
