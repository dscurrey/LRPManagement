using LRPManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRPManagement.Data.Skills
{
    /// <summary>
    /// Repository for accessing and performing database operations with Skills
    /// </summary>
    public interface ISkillRepository
    {
        Task<List<Skill>> GetAll();
        Task<Skill> GetSkill(int id);
        Task<Skill> GetSkillRef(int id);
        Task<int> GetCount();
        void InsertSkill(Skill skill);
        Task DeleteSkill(int id);
        void UpdateSkill(Skill skill);
        Task Save();
    }
}