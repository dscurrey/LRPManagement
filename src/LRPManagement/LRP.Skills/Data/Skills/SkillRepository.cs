using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRP.Skills.Models;

namespace LRP.Skills.Data.Skills
{
    public class SkillRepository : ISkillRepository
    {
        private readonly SkillDbContext _context;

        public SkillRepository(SkillDbContext context)
        {
            _context = context;
        }

        public async Task<List<Skill>> GetAll()
        {
            return await _context.Skill.ToListAsync();
        }

        public async Task<Skill> GetSkill(int id)
        {
            return await _context.Skill.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task InsertSkill(Skill skill)
        {
            _context.Skill.Add(skill);
        }

        public async void DeleteSkill(int id)
        {
            var skill = await _context.Skill.FirstOrDefaultAsync(p => p.Id == id);
            _context.Skill.Remove(skill);
        }

        public async void UpdateSkill(Skill skill)
        {
            _context.Entry(skill).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
