using LRP.Players.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRP.Players.Data.Players
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly PlayerDbContext _context;

        public PlayerRepository(PlayerDbContext context)
        {
            _context = context;
        }

        public async Task<List<Player>> GetAll()
        {
            return await _context.Player.ToListAsync();
        }

        public async Task<Player> GetPlayer(int id)
        {
            return await _context.Player.FirstOrDefaultAsync(p => p.Id == id);
        }

        public void InsertPlayer(Player player)
        {
            _context.Player.Add(player);
        }

        public async void DeletePlayer(int id)
        {
            var player = await _context.Player.FirstOrDefaultAsync(p => p.Id == id);
            player.IsActive = false;
            player.FirstName = "ANONYMOUS";
            player.LastName = "ANONYMOUS";
            player.AccountRef = "ANONYMOUS";
            _context.Player.Update(player);
        }

        public void UpdatePlayer(Player player)
        {
            _context.Entry(player).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
