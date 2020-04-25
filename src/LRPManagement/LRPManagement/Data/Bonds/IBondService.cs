using LRPManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRPManagement.Data.Bonds
{
    public interface IBondService
    {
        Task<List<Bond>> Get();
        Task<Bond> Get(int id);
        Task<Bond> Create(Bond bond);
        Task<bool> Delete(int id);
    }
}
