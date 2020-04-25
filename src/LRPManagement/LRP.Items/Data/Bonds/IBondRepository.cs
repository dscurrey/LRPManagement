using LRP.Items.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRP.Items.Data.Bonds
{
    public interface IBondRepository
    {
        Task<List<Bond>> GetAll();
        Task<Bond> Get(int id);
        Task<List<Bond>> GetForItem(int itemId);
        Task<List<Bond>> GetForCharacter(int charId);
        void Insert(Bond bond);
        Task Delete(int id);
        Task Save();
    }
}
