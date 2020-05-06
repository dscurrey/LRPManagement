using LRPManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRPManagement.Data.CharacterSkills
{
    /// <summary>
    /// Repository for accessing and performing database operations with Character Skills (Character - Skill link entries)
    /// </summary>
    public interface ICharacterSkillRepository
    {
        /// <summary>
        /// Creates a new CharacterSkill using Ids
        /// </summary>
        /// <param name="skillId">Id of the chosen skill</param>
        /// <param name="charId">Id of the chosen character</param>
        void AddSkillToCharacter(int skillId, int charId);
        Task Save();
        Task<CharacterSkill> Get(int id);
        Task<List<CharacterSkill>> Get();
        Task<CharacterSkill> GetMatch(int charId, int skillId);
        Task Delete(int id);
    }
}