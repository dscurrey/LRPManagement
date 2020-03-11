using LRP.Skills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRP.Skills.Data.Skills
{
    public class FakeSkillRepository : ISkillRepository
    {
        private readonly List<Skill> _skills;

        public FakeSkillRepository(List<Skill> skills)
        {
            _skills = skills;
        }

        public void DeleteSkill(int id)
        {
            var skill = _skills.Find(s => s.Id == id);
            _skills.Remove(skill);
        }

        public Task<List<Skill>> GetAll()
        {
            return Task.FromResult(_skills.ToList());
        }

        public Task<Skill> GetSkill(int id)
        {
            return Task.FromResult(_skills.FirstOrDefault(s => s.Id == id));
        }

        public void InsertSkill(Skill skill)
        {
            _skills.Add(skill);
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }

        public void UpdateSkill(Skill skill)
        {
            throw new NotImplementedException();
        }
    }
}
