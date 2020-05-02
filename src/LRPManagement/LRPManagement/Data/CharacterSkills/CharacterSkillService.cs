using LRPManagement.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace LRPManagement.Data.CharacterSkills
{
    public class CharacterSkillService : ICharacterSkillService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<CharacterSkillService> _logger;

        public HttpClient Client { get; set; }

        public CharacterSkillService(IHttpClientFactory clientFactory,
            IConfiguration config,
            ILogger<CharacterSkillService> logger)
        {
            _clientFactory = clientFactory;
            _config = config;
            _logger = logger;
        }

        public async Task<CharacterSkill> Create(CharacterSkill charSkill)
        {
            var client = GetHttpClient("StandardRequest");
            var resp = await client.PostAsync("api/characterskills/", charSkill, new JsonMediaTypeFormatter());
            if (resp.IsSuccessStatusCode) return charSkill;

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var client = GetHttpClient("StandardRequest");
            var resp = await client.DeleteAsync("api/characterskills/" + id);
            if (resp.IsSuccessStatusCode) return true;

            return false;
        }

        public async Task<List<CharacterSkill>> Get()
        {
            try
            {
                var client = GetHttpClient("StandardRequest");
                var resp = await client.GetAsync("api/characterskills/");
                if (resp.IsSuccessStatusCode)
                {
                    var charSkills = await resp.Content.ReadAsAsync<List<CharacterSkill>>();
                    return charSkills;
                }
            }
            catch (TaskCanceledException e)
            {
                _logger.LogError(ToString(), "Task Cancelled Error", e);
            }

            return null;
        }

        public async Task<CharacterSkill> Get(int id)
        {
            try
            {
                var client = GetHttpClient("StandardRequest");
                var resp = await client.GetAsync("api/characterskills/" + id);
                if (resp.IsSuccessStatusCode)
                {
                    var charSkill = await resp.Content.ReadAsAsync<CharacterSkill>();
                    return charSkill;
                }
            }
            catch (TaskCanceledException e)
            {
                _logger.LogError(ToString(), "Task Cancelled Error", e);
            }

            return null;
        }

        private HttpClient GetHttpClient(string s)
        {
            if (Client != null && _clientFactory == null) return Client;

            var client = _clientFactory.CreateClient(s);
            client.BaseAddress = new Uri(_config["SkillsURL"]);
            return client;
        }
    }
}