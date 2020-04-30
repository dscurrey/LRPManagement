using System.Collections.Generic;
using System.Linq;
using LRPManagement.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LRPManagement.Data.CharacterSkills
{
    public class CharacterSkillRepository : ICharacterSkillRepository
    {
        private readonly LrpDbContext _context;

        public CharacterSkillRepository(LrpDbContext context)
        {
            _context = context;
        }

        public void AddSkillToCharacter(int skillId, int charId)
        {
            var charSkill = new CharacterSkill
            {
                CharacterId = charId,
                SkillId = skillId
            };
            _context.CharacterSkills.Add(charSkill);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<CharacterSkill> Get(int id)
        {
            return await _context.CharacterSkills.Include(c => c.Character).Include(c => c.Skill).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<CharacterSkill>> Get()
        {
            return await _context.CharacterSkills.Include(c => c.Character).Include(c => c.Skill).ToListAsync();
        }

        public async Task<CharacterSkill> GetMatch(int charId, int skillId)
        {
            return await _context.CharacterSkills.Where(cs => cs.SkillId == skillId).Where
                (cs => cs.CharacterId == charId).FirstOrDefaultAsync();
        }

        public async Task Delete(int id)
        {
            var charSkill = await _context.CharacterSkills.FirstOrDefaultAsync(c => c.Id == id);
            _context.CharacterSkills.Remove(charSkill);
        }
    }
}
