using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Data.Skills
{
    public interface ISkillService
    {
        Task<List<SkillDTO>> GetAll();
        Task<SkillDTO> GetSkill(int id);
    }
}
