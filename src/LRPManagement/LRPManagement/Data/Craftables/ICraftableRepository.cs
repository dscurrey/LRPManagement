using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRPManagement.Models;
using DTO;

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
