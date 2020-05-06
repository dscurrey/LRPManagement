using DTO;
using LRPManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRPManagement.Data.Craftables
{
    /// <summary>
    /// Service for accessing and performing API operations with Craftables (Items)
    /// </summary>
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