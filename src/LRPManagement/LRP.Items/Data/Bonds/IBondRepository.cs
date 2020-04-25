using System.Collections.Generic;
using System.Threading.Tasks;
using LRP.Items.Models;

namespace LRP.Items.Data.Bonds
{
    public interface IBondRepository
    {
        Task<List<Bond>> GetAll();
        Task<Bond> Get(int id);
        Task<List<Bond>> GetForItem(int itemId);
        Task<List<Bond>> GetForPlayer(int playerId);
        void Insert(Bond bond);
        Task Delete(int id);
        Task Save();
    }
}
