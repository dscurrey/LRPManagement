using System.Collections.Generic;
using System.Threading.Tasks;
using LRPManagement.Models;

namespace LRPManagement.Data.CharacterSkills
{
    public interface ICharacterSkillRepository
    {
        void AddSkillToCharacter(int skillId, int charId);
        Task Save();
        Task<CharacterSkill> Get(int id);
        Task<List<CharacterSkill>> Get();
        Task Delete(int id);
    }
}
