using System.Collections.Generic;
using System.Threading.Tasks;
using LRP.Skills.Models;

namespace LRP.Skills.Data.CharacterSkills
{
    public interface ICharacterSkillService
    {
        Task<List<CharacterSkill>> Get();
        Task<CharacterSkill> Get(int id);
        Task<CharacterSkill> Create(CharacterSkill charSkill);
        Task<bool> Delete(int id);
    }
}
