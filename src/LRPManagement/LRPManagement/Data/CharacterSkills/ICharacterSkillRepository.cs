using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Data.CharacterSkills
{
    public interface ICharacterSkillRepository
    {
        void AddSkillToCharacter(int skillId, int charId);
        Task Save();
    }
}
