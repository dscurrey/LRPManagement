using LRPManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRPManagement.Data.Craftables
{
    /// <summary>
    /// Repository for accessing and performing database operations with Craftables (Items)
    /// </summary>
    public interface ICraftableRepository
    {
        Task<List<Craftable>> GetAll();
        Task<Craftable> GetCraftable(int id);
        Task<Craftable> GetCraftableRef(int id);
        Task<int> GetCount();
        void InsertCraftable(Craftable craftable);
        Task DeleteCraftable(int id);
        void UpdateCraftable(Craftable craftable);
        Task Save();
    }
}