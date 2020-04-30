using DTO;
using LRP.Players.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
            _context.Entry(player).State = EntityState.Added;
            _context.Player.Add(player);
        }

        public async Task DeletePlayer(int id)
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
            var dbPlayer = _context.Player.First(p => p.Id == player.Id);
            _context.Entry(dbPlayer).CurrentValues.SetValues(player);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdatePlayer(PlayerDTO player)
        {
            var updPlayer = new Player
            {
                AccountRef = player.AccountRef,
                FirstName = player.FirstName,
                IsActive = player.IsActive,
                Id = player.Id,
                LastName = player.LastName
            };
            UpdatePlayer(updPlayer);
        }
    }
}
