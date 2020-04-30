using LRP.Items.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRP.Items.Data.Craftables
{
    public class CraftableRepository : ICraftableRepository
    {
        private readonly ItemsDbContext _context;

        public CraftableRepository(ItemsDbContext context)
        {
            _context = context;
        }

        public async Task DeleteCraftable(int id)
        {
            var item = await GetCraftable(id);
            _context.Craftables.Remove(item);
        }

        public async Task<List<Craftable>> GetAll()
        {
            return await _context.Craftables.ToListAsync();
        }

        public async Task<Craftable> GetCraftable(int id)
        {
            return await _context.Craftables.FirstOrDefaultAsync(c => c.Id == id);
        }

        public void InsertCraftable(Craftable craftable)
        {
            _context.Craftables.Add(craftable);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateCraftable(Craftable craftable)
        {
            var dbItem = _context.Craftables.First(c => c.Id == craftable.Id);
            _context.Entry(dbItem).CurrentValues.SetValues(craftable);
        }
    }
}
