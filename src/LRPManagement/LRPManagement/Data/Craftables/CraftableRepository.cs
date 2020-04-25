using LRPManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            _context.Craftables.Update(craftable);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
