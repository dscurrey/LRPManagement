using LRPManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Data.Players
{
    public class FakePlayerRepository : IPlayerRepository
    {
        private readonly List<Player> _list;

        public FakePlayerRepository(List<Player> list)
        {
            _list = list;
        }

        public async Task<List<Player>> GetAll()
        {
            return await Task.FromResult(_list.ToList());
        }

        public async Task<Player> GetPlayer(int id)
        {
            return await Task.FromResult(_list.FirstOrDefault(p => p.Id == id));
        }

        public async Task<Player> GetPlayerRef(int id)
        {
            return await Task.FromResult(_list.FirstOrDefault(p => p.PlayerRef == id));
        }

        public async Task<Player> GetPlayerAccountRef(string id)
        {
            return await Task.FromResult(_list.FirstOrDefault(p => p.PlayerRef.ToString().ToLower().Equals(id.ToLower())));
        }

        public void InsertPlayer(Player player)
        {
            _list.Add(player);
        }

        public async Task DeletePlayer(int id)
        {
            var player = await Task.FromResult(_list.FirstOrDefault(p => p.Id == id));
            _list.Remove(player);
        }

        public async Task AnonPlayer(int id)
        {
            throw new NotImplementedException();
        }

        public async Task DeletePlayerRef(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlayer(Player player)
        {
            var tgtPlayer = _list.FirstOrDefault(p => p.Id == player.Id);
            _list.Remove(tgtPlayer);
            _list.Add(player);
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }
    }
}
