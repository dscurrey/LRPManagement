using LRPManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Data.Skills
{
    public class SkillRepository : ISkillRepository
    {
        private readonly LrpDbContext _context;

        public SkillRepository(LrpDbContext context)
        {
            _context = context;
        }

        public async Task DeleteSkill(int id)
        {
            var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == id);
            _context.Skills.Remove(skill);
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
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Skills ON");
            await _context.SaveChangesAsync();
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Skills OFF");
        }

        public void UpdateSkill(Skill skill)
        {
            var dbSkill = _context.Skills.First(s => s.Id == skill.Id);
            _context.Entry(dbSkill).CurrentValues.SetValues(skill);
        }
    }
}
