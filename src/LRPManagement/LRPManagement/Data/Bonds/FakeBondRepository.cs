using LRPManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Data.Bonds
{
    public class FakeBondRepository : IBondRepository
    {
        private readonly List<Bond> _bonds;

        public FakeBondRepository(List<Bond> bonds)
        {
            _bonds = bonds;
        }

        public async Task<List<Bond>> GetAll()
        {
            return await Task.FromResult(_bonds.ToList());
        }

        public async Task<Bond> Get(int id)
        {
            return await Task.FromResult(_bonds.FirstOrDefault(b => b.Id == id));
        }

        public async Task<List<Bond>> GetForItem(int itemId)
        {
            return await Task.FromResult(_bonds.Where(b => b.ItemId == itemId).ToList());
        }

        public Task<List<Bond>> GetForPlayer(int playerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Bond> GetMatch(int charId, int itemId)
        {
            return await Task.FromResult
                (_bonds.Where(b => b.CharacterId == charId).FirstOrDefault(b => b.ItemId == itemId));
        }

        public void Insert(Bond bond)
        {
            _bonds.Add(bond);
        }

        public async Task Delete(int id)
        {
            var bond = _bonds.Find(b => b.Id == id);
            await Task.FromResult(_bonds.Remove(bond));
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }
    }
}