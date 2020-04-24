using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRPManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LRPManagement.Data.Craftables
{
    public class CraftableRepository : ICraftableRepository
    {
        private LrpDbContext _context;

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
