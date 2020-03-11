using LRP.Players.Models;
using System;
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

        public void DeletePlayer(int id)
        {
            var player = _players.Find(p => p.Id == id);
            _players.Remove(player);
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

        public Task Save()
        {
            return Task.CompletedTask;
        }

        public void UpdatePlayer(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
