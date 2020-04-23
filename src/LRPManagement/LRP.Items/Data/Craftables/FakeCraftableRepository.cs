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
            throw new NotImplementedException();
        }

        public async Task<Craftable> GetCraftable(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertCraftable(Craftable craftable)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteCraftable(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateCraftable(Craftable craftable)
        {
            throw new NotImplementedException();
        }

        public async Task Save()
        {
            throw new NotImplementedException();
        }
    }
}
