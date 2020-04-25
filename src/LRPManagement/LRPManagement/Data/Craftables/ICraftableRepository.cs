using LRPManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRPManagement.Data.Craftables
{
    public interface ICraftableRepository
    {
        Task<List<Craftable>> GetAll();
        Task<Craftable> GetCraftable(int id);
        void InsertCraftable(Craftable craftable);
        Task DeleteCraftable(int id);
        void UpdateCraftable(Craftable craftable);
        Task Save();
    }
}
