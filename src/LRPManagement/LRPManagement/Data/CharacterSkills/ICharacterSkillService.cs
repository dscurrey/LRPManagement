using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRPManagement.Models;

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
