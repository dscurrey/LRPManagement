using LRPManagement.Models;
using System.Threading.Tasks;

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
            _context.CharacterSkills.AddAsync(charSkill);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
