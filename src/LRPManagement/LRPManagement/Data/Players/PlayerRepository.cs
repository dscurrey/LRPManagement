using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRPManagement.Models;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
        }

        public void InsertPlayer(Player player)
        {
            _context.Players.AddAsync(player);
        }

        public async Task DeletePlayer(int id)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
            _context.Players.Remove(player);
        }

        public void UpdatePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Player> GetPlayerRef(int id)
        {
            return await _context.Players.FirstOrDefaultAsync(p => p.PlayerRef == id);
        }
    }
}
