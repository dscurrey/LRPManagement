using System.Collections.Generic;
using System.Threading.Tasks;
using LRPManagement.Models;

namespace LRPManagement.Data.Bonds
{
    public interface IBondRepository
    {
        Task<List<Bond>> GetAll();
        Task<Bond> Get(int id);
        Task<List<Bond>> GetForItem(int itemId);
        Task<List<Bond>> GetForPlayer(int playerId);
        Task<Bond> GetMatch(int charId, int itemId);
        void Insert(Bond bond);
        Task Delete(int id);
        Task Save();
    }
}
