using LRPManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRPManagement.Data.CharacterSkills
{
    public interface ICharacterSkillService
    {
        Task<List<CharacterSkill>> Get();
        Task<CharacterSkill> Get(int id);
        Task<CharacterSkill> Create(CharacterSkill charSkill);
        Task<bool> Delete(int id);
    }
}
