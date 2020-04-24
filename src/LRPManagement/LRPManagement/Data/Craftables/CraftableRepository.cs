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

        public async Task<Craftable> GetCraftableRef(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertCraftable(Craftable craftable)
        {
            _context.Craftables.Add(craftable);
        }

        public async Task DeleteCraftable(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateCraftable(Craftable craftable)
        {
            throw new NotImplementedException();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
