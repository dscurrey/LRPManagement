using DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace LRPManagement.Data.Skills
{
    public class SkillService : ISkillService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<SkillService> _logger;
        private readonly IConfiguration _config;

        public HttpClient Client { get; set; }

        public SkillService(IHttpClientFactory clientFactory, ILogger<SkillService> logger, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _config = config;
        }

        public async Task<SkillDTO> CreateSkill(SkillDTO skill)
        {
            try
            {
                var client = GetHttpClient("StandardRequest");
                var resp = await client.PostAsync("api/skills/", skill, new JsonMediaTypeFormatter());
                if (resp.IsSuccessStatusCode)
                {
                    return skill;
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning("" + ex);
            }

            return null;
        }

        public async Task<int> DeleteSkill(int id)
        {
            try
            {
                var client = GetHttpClient("StandardRequest");
                var resp = await client.DeleteAsync("api/skills" + id);
                if (resp.IsSuccessStatusCode)
                {
                    return id;
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning("" + ex);
            }

            return 0;
        }

        public async Task<List<SkillDTO>> GetAll()
        {
            try
            {
                var client = GetHttpClient("StandardRequest");
                var resp = await client.GetAsync("api/skills");
                if (resp.IsSuccessStatusCode)
                {
                    var skills = await resp.Content.ReadAsAsync<List<SkillDTO>>();
                    return skills;
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning("" + ex);
            }

            return null;
        }

        public async Task<SkillDTO> GetSkill(int id)
        {
            try
            {
                var client = GetHttpClient("StandardRequest");
                var resp = await client.GetAsync("api/skills/" + id);
                if (resp.IsSuccessStatusCode)
                {
                    var skills = await resp.Content.ReadAsAsync<SkillDTO>();
                    return skills;
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning("" + ex);
            }
            return null;
        }

        public async Task<SkillDTO> UpdateSkill(SkillDTO skill)
        {
            try
            {
                var client = GetHttpClient("StandardRequest");
                var resp = await client.PutAsync("api/skills" + skill.Id, skill, new JsonMediaTypeFormatter());
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning("" + ex);
            }

            return null;
        }

        private HttpClient GetHttpClient(string s)
        {
            if (Client != null && _clientFactory == null)
            {
                return Client;
            }

            var client = _clientFactory.CreateClient(s);
            client.BaseAddress = new Uri(_config["SkillsURL"]);
            return client;
        }
    }
}
