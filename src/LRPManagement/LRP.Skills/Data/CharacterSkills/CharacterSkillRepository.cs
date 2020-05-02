using LRP.Skills.Models;
using LRPManagement.Data.CharacterSkills;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRP.Skills.Data.CharacterSkills
{
    public class CharacterSkillRepository : ICharacterSkillRepository
    {
        private readonly SkillDbContext _context;

        public CharacterSkillRepository(SkillDbContext context)
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
            return await _context.CharacterSkills.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<CharacterSkill>> Get()
        {
            return await _context.CharacterSkills.ToListAsync();
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

        public void Insert(CharacterSkill characterSkill)
        {
            _context.CharacterSkills.Add(characterSkill);
        }
    }
}
