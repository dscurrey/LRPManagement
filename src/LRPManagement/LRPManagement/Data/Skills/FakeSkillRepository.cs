﻿using LRPManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Data.Skills
{
    public class FakeSkillRepository : ISkillRepository
    {
        private readonly List<Skill> _list;

        public FakeSkillRepository(List<Skill> skills)
        {
            _list = skills;
        }

        public async Task<List<Skill>> GetAll()
        {
            return await Task.FromResult(_list.ToList());
        }

        public async Task<Skill> GetSkill(int id)
        {
            return await Task.FromResult(_list.FirstOrDefault(s => s.Id == id));
        }

        public async Task<Skill> GetSkillRef(int id)
        {
            return await Task.FromResult(_list.FirstOrDefault(s => s.SkillRef == id));
        }

        public async Task<int> GetCount()
        {
            return await Task.FromResult(_list.Count());
        }

        public void InsertSkill(Skill skill)
        {
            _list.Add(skill);
        }

        public async Task DeleteSkill(int id)
        {
            var skill = await Task.FromResult(_list.FirstOrDefault(s => s.Id == id));
            _list.Remove(skill);
        }

        public void UpdateSkill(Skill skill)
        {
            var tgtSkill = _list.FirstOrDefault(s => s.Id == skill.Id);
            _list.Remove(tgtSkill);
            _list.Add(skill);
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }
    }
}