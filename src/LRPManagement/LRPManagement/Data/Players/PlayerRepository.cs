using LRPManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Data.Players
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly LrpDbContext _context;

        public PlayerRepository(LrpDbContext context)
        {
            _context = context;
        }

        public async Task<List<Player>> GetAll()
        {
            return await _context.Players.ToListAsync();
        }

        public async Task<Player> GetPlayer(int id)
        {
            return await _context.Players.Include(p => p.Characters).FirstOrDefaultAsync(p => p.Id == id);
        }

        public void InsertPlayer(Player player)
        {
            _context.Players.Add(player);
        }

        public async Task DeletePlayer(int id)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
            _context.Remove(player);
        }

        public async Task AnonPlayer(int id)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
            player.FirstName = "ANONYMOUS";
            player.LastName = "ANONYMOUS";
            player.AccountRef = "ANONYMOUS";
            _context.Players.Update(player);
        }

        public void UpdatePlayer(Player player)
        {
            var dbPlayer = _context.Players.First(p => p.Id == player.Id);
            _context.Entry(dbPlayer).CurrentValues.SetValues(player);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Player> GetPlayerRef(int id)
        {
            return await _context.Players.FirstOrDefaultAsync(p => p.PlayerRef == id);
        }

        public async Task<Player> GetPlayerAccountRef(string id)
        {
            return await _context.Players.FirstOrDefaultAsync(p => p.AccountRef == id);
        }

        public async Task DeletePlayerRef(int id)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.PlayerRef == id);
            _context.Players.Remove(player);
        }
    }
}