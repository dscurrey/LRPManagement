using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRPManagement.Models;

namespace LRPManagement.Data.Skills
{
    public interface ISkillRepository
    {
        Task<List<Skill>> GetAll();
        Task<Skill> GetSkill(int id);
        Task<Skill> GetSkillRef(int id);
        void InsertSkill(Skill skill);
        Task DeleteSkill(int id);
        void UpdateSkill(Skill skill);
        Task Save();
    }
}
