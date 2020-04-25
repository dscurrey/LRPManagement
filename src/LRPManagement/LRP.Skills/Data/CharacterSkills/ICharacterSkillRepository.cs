using System.Collections.Generic;
using System.Threading.Tasks;
using LRP.Skills.Models;

namespace LRPManagement.Data.CharacterSkills
{
    public interface ICharacterSkillRepository
    {
        void AddSkillToCharacter(int skillId, int charId);
        Task Save();
        Task<CharacterSkill> Get(int id);
        Task<List<CharacterSkill>> Get();
        Task<CharacterSkill> GetMatch(int charId, int skillId);
        Task Delete(int id);
    }
}
