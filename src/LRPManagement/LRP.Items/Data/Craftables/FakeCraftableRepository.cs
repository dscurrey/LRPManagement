using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRP.Items.Models;

namespace LRP.Items.Data.Craftables
{
    public class FakeCraftableRepository : ICraftableRepository
    {
        private readonly List<Craftable> _items;

        public FakeCraftableRepository(List<Craftable> items)
        {
            _items = items;
        }

        public async Task<List<Craftable>> GetAll()
        {
            return await Task.FromResult(_items.ToList());
        }

        public async Task<Craftable> GetCraftable(int id)
        {
            return await Task.FromResult(_items.FirstOrDefault(i => i.Id == id));
        }

        public void InsertCraftable(Craftable craftable)
        {
            _items.Add(craftable);
        }

        public async Task DeleteCraftable(int id)
        {
            var item = await GetCraftable(id);
            await Task.FromResult(_items.Remove(item));
        }

        public void UpdateCraftable(Craftable craftable)
        {
            var tgt = _items.Find(s => s.Id == craftable.Id);
            _items.Remove(tgt);
            _items.Add(craftable);
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }
    }
}
