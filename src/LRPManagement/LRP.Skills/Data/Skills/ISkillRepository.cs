using LRP.Skills.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRP.Skills.Data.Skills
{
    public interface ISkillRepository
    {
        Task<List<Skill>> GetAll();
        Task<Skill> GetSkill(int id);
        void InsertSkill(Skill skill);
        void DeleteSkill(int id);
        void UpdateSkill(Skill skill);
        Task Save();
    }
}