﻿using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LRPManagement.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LRPManagement.Data.Skills
{
    public class SkillService : ISkillService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _httpClient;
        private readonly ILogger<SkillService> _logger;
        private readonly IConfiguration _config;

        public SkillService(IHttpClientFactory clientFactory, ILogger<SkillService> logger, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _config = config;

            _httpClient = GetHttpClient("StandardRequest");
        }

        public async Task<List<SkillDTO>> GetAll()
        {
            try
            {
                var resp = await _httpClient.GetAsync("api/skills");
                if (resp.IsSuccessStatusCode)
                {
                    var skills = await resp.Content.ReadAsAsync<List<SkillDTO>>();
                    return skills;
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning(""+ex);
            }

            return null;
        }

        public async Task<SkillDTO> GetSkill(int id)
        {
            try
            {
                var resp = await _httpClient.GetAsync("api/skills/" + id);
                if (resp.IsSuccessStatusCode)
                {
                    var skills = await resp.Content.ReadAsAsync<SkillDTO>();
                    return skills;
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning(""+ex);
            }
            return null;
        }

        private HttpClient GetHttpClient(string s)
        {
            var client = _clientFactory.CreateClient(s);
            client.BaseAddress = new Uri(_config["SkillsURL"]);
            return client;
        }
    }
}
