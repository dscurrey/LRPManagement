using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRPManagement.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace LRPManagement.Data.Skills
{
    public class FakeSkillRepository : ISkillRepository
    {
        private List<Skill> _list;

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
