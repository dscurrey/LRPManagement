using LRPManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRPManagement.Data.Bonds
{
    /// <summary>
    /// Service for accessing and performing API operations with Bonds in the Items API
    /// </summary>
    public interface IBondService
    {
        Task<List<Bond>> Get();
        Task<Bond> Get(int id);
        Task<Bond> Create(Bond bond);
        Task<bool> Delete(int id);
    }
}