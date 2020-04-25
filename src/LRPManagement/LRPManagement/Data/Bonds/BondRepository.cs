using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRPManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LRPManagement.Data.Bonds
{
    public class BondRepository : IBondRepository
    {
        private readonly LrpDbContext _context;

        public BondRepository(LrpDbContext context)
        {
            _context = context;
        }

        public async Task<List<Bond>> GetAll()
        {
            return await _context.Bond.Include(b => b.Item).Include(b => b.Character).ToListAsync();
        }

        public async Task<Bond> Get(int id)
        {
            return await _context.Bond.Include(b => b.Item).Include(b => b.Character).FirstOrDefaultAsync(b => b.Id == id);
        }

        public void Insert(Bond bond)
        {
            _context.Bond.Add(bond);
        }

        public async Task Delete(int id)
        {
            var bond = await _context.Bond.FirstOrDefaultAsync(b => b.Id == id);
            _context.Bond.Remove(bond);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Bond>> GetForItem(int itemId)
        {
            return await _context.Bond.Include(b => b.Item).Include(b => b.Character).Where(b => b.ItemId == itemId).ToListAsync();
        }

        public async Task<List<Bond>> GetForPlayer(int playerId)
        {
            return await _context.Bond.Include(b => b.Item).Include(b => b.Character).Where(b => b.CharacterId == playerId).ToListAsync();
        }

        public async Task<Bond> GetMatch(int charId, int itemId)
        {
            return await _context.Bond.Where(b => b.ItemId == itemId).Where
                (b => b.CharacterId == charId).FirstOrDefaultAsync();
        }
    }
}
