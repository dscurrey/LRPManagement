using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LRPManagement.Data.Skills
{
    public class SkillService : ISkillService
    {
        private readonly HttpClient _httpClient;

        public SkillService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SkillDTO>> GetAll()
        {
            var resp = await _httpClient.GetAsync("api/skills");
            if (resp.IsSuccessStatusCode)
            {
                var skills = await resp.Content.ReadAsAsync<List<SkillDTO>>();
                return skills;
            }
            return null;
        }

        public async Task<SkillDTO> GetSkill(int id)
        {
            var resp = await _httpClient.GetAsync("api/skills/" + id);
            if (resp.IsSuccessStatusCode)
            {
                var skills = await resp.Content.ReadAsAsync<SkillDTO>();
                return skills;
            }
            return null;
        }
    }
}
