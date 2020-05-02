using DTO;
using LRP.Players.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRP.Players.Data.Players
{
    public class FakePlayerRepository : IPlayerRepository
    {
        private readonly List<Player> _players;

        public FakePlayerRepository(List<Player> players)
        {
            _players = players;
        }

        public Task DeletePlayer(int id)
        {
            var player = _players.Find(p => p.Id == id);
            return Task.FromResult(_players.Remove(player));
        }

        public Task<List<Player>> GetAll()
        {
            return Task.FromResult(_players.ToList());
        }

        public Task<Player> GetPlayer(int id)
        {
            return Task.FromResult(_players.FirstOrDefault(p => p.Id == id));
        }

        public void InsertPlayer(Player player)
        {
            _players.Add(player);
        }

        public void UpdatePlayer(PlayerDTO player)
        {
            var updPlayer = new Player
            {
                AccountRef = player.AccountRef,
                FirstName = player.FirstName,
                Id = player.Id,
                IsActive = player.IsActive,
                LastName = player.LastName
            };
            UpdatePlayer(updPlayer);
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }

        public void UpdatePlayer(Player player)
        {
            var tgt = _players.Find(s => s.Id == player.Id);
            _players.Remove(tgt);
            _players.Add(player);
        }
    }
}