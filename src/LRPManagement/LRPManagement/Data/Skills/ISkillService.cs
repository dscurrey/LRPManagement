using DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

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
