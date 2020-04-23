using LRP.Items.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LRP.Items.Data.Craftables
{
    public class CraftableRepository : ICraftableRepository
    {
        private readonly ItemsDbContext _context;
        
        public CraftableRepository(ItemsDbContext context)
        {
            _context = context;
        }

        public void DeleteCraftable(int id)
        {
            throw new NotImplementedException();
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
            _context.Craftables.Update(craftable);
        }
    }
}
