using LRP.Skills.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRPManagement.Data.CharacterSkills
{
    /// <summary>
    /// Repository for working with the database and CharacterSkills
    /// </summary>
    public interface ICharacterSkillRepository
    {
        void AddSkillToCharacter(int skillId, int charId);
        void Insert(CharacterSkill characterSkill);
        Task Save();
        Task<CharacterSkill> Get(int id);
        Task<List<CharacterSkill>> Get();
        Task<CharacterSkill> GetMatch(int charId, int skillId);
        Task Delete(int id);
    }
}