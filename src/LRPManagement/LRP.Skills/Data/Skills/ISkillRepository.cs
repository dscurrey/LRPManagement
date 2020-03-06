using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRP.Skills.Models;

namespace LRP.Skills.Data.Skills
{
    public interface ISkillRepository
    {
        Task<List<Skill>> GetAll();
        Task<Skill> GetSkill(int id);
        Task InsertSkill(Skill skill);
        void DeleteSkill(int id);
        void UpdateSkill(Skill skill);
        Task Save();
    }
}
