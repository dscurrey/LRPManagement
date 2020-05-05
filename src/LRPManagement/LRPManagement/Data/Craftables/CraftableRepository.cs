using LRPManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Data.Craftables
{
    public class CraftableRepository : ICraftableRepository
    {
        private readonly LrpDbContext _context;

        public CraftableRepository(LrpDbContext context)
        {
            _context = context;
        }

        public async Task<List<Craftable>> GetAll()
        {
            return await _context.Craftables.ToListAsync();
        }

        public async Task<Craftable> GetCraftable(int id)
        {
            return await _context.Craftables.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Craftable> GetCraftableRef(int id)
        {
            return await _context.Craftables.FirstOrDefaultAsync(c => c.ItemRef == id);
        }

        public void InsertCraftable(Craftable craftable)
        {
            _context.Craftables.Add(craftable);
        }

        public async Task DeleteCraftable(int id)
        {
            var item = await _context.Craftables.FirstOrDefaultAsync(c => c.Id == id);
            _context.Craftables.Remove(item);
        }

        public void UpdateCraftable(Craftable craftable)
        {
            var dbItem = _context.Craftables.First(c => c.Id == craftable.Id);
            _context.Entry(dbItem).CurrentValues.SetValues(craftable);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCount()
        {
            return await _context.Craftables.CountAsync();
        }
    }
}