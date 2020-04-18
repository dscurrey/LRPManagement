using LRPManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LRPManagement.Data.Skills
{
    public class SkillRepository : ISkillRepository
    {

        private readonly LrpDbContext _context;

        public SkillRepository(LrpDbContext context)
        {
            _context = context;
        }

        public void DeleteSkill(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Skill>> GetAll()
        {
            return await _context.Skills.ToListAsync();
        }

        public async Task<Skill> GetSkill(int id)
        {
            return await _context.Skills.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Skill> GetSkillRef(int id)
        {
            return await _context.Skills.FirstOrDefaultAsync(s => s.SkillRef == id);
        }

        public void InsertSkill(Skill skill)
        {
            _context.Skills.Add(skill);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateSkill(Skill skill)
        {
            throw new NotImplementedException();
        }
    }
}
