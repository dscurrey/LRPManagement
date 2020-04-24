using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using LRPManagement.Models;

namespace LRPManagement.Data.Craftables
{
    public interface ICraftableService
    {
        Task<List<CraftableDTO>> GetAll();
        Task<CraftableDTO> GetCraftable(int id);
        Task<CraftableDTO> UpdateCraftable(CraftableDTO craftable);
        Task<CraftableDTO> UpdateCraftable(Craftable craftable);
        Task<CraftableDTO> CreateCraftable(CraftableDTO craftable);
        Task<int> DeleteCraftable(int id);
    }
}
