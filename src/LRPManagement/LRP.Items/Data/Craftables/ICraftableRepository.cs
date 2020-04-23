using System.Collections.Generic;
using System.Threading.Tasks;
using LRP.Items.Models;

namespace LRP.Items.Data.Craftables
{
    public interface ICraftableRepository
    {
        Task<List<Craftable>> GetAll();
        Task<Craftable> GetCraftable(int id);
        void InsertCraftable(Craftable craftable);
        void DeleteCraftable(int id);
        void UpdateCraftable(Craftable craftable);
        Task Save();
    }
}
