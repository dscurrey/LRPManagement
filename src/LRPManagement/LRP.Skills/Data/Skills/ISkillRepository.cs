using LRP.Skills.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRP.Skills.Data.Skills
{
    /// <summary>
    /// Repository for working with skills and the database
    /// </summary>
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