﻿using LRPManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Data.Craftables
{
    public class FakeCraftableRepository : ICraftableRepository
    {
        private readonly List<Craftable> _list;

        public FakeCraftableRepository(List<Craftable> list)
        {
            _list = list;
        }

        public async Task<List<Craftable>> GetAll()
        {
            return await Task.FromResult(_list.ToList());
        }

        public async Task<Craftable> GetCraftable(int id)
        {
            return await Task.FromResult(_list.FirstOrDefault(c => c.Id == id));
        }

        public async Task<Craftable> GetCraftableRef(int id)
        {
            return await Task.FromResult(_list.FirstOrDefault(c => c.ItemRef == id));
        }

        public async Task<int> GetCount()
        {
            return await Task.FromResult(_list.Count);
        }

        public void InsertCraftable(Craftable craftable)
        {
            _list.Add(craftable);
        }

        public async Task DeleteCraftable(int id)
        {
            var item = _list.FirstOrDefault(c => c.Id == id);
            await Task.FromResult(_list.Remove(item));
        }

        public void UpdateCraftable(Craftable craftable)
        {
            var item = _list.FirstOrDefault(c => c.Id == craftable.Id);
            _list.Remove(item);
            _list.Add(craftable);
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }
    }
}