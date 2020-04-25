using LRP.Items.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRP.Items.Data.Bonds
{
    public class BondRepository : IBondRepository
    {
        private readonly ItemsDbContext _context;

        public BondRepository(ItemsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Bond>> GetAll()
        {
            return await _context.Bonds.ToListAsync();
        }

        public async Task<Bond> Get(int id)
        {
            return await _context.Bonds.FirstOrDefaultAsync(b => b.Id == id);
        }

        public void Insert(Bond bond)
        {
            var newBond = new Bond
            {
                CharacterId = bond.CharacterId,
                ItemId = bond.ItemId
            };

            _context.Bonds.Add(newBond);
        }

        public async Task Delete(int id)
        {
            var bond = await _context.Bonds.FirstOrDefaultAsync(b => b.Id == id);
            _context.Bonds.Remove(bond);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Bond>> GetForItem(int itemId)
        {
            return await _context.Bonds.Where(b => b.ItemId == itemId).ToListAsync();
        }

        public async Task<List<Bond>> GetForCharacter(int charId)
        {
            return await _context.Bonds.Where(b => b.CharacterId == charId).ToListAsync();
        }
    }
}
