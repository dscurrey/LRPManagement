using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRPManagement.Models;

namespace LRPManagement.Data.Skills
{
    public interface ISkillService
    {
        Task<List<SkillDTO>> GetAll();
        Task<SkillDTO> GetSkill(int id);
        Task<SkillDTO> UpdateSkill(SkillDTO skill);
        Task<SkillDTO> CreateSkill(SkillDTO skill);
        Task<int> DeleteSkill(int id);
    }
}
