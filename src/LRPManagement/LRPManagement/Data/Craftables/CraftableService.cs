using DTO;
using LRPManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using LRPManagement.Data.Characters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LRPManagement.Data.Craftables
{
    public class CraftableService : ICraftableService
    {
        private IHttpClientFactory _clientFactory;
        private IConfiguration _config;
        private ILogger<CraftableService> _logger;
        private HttpClient _httpClient;

        public CraftableService(IHttpClientFactory clientFactory, IConfiguration config, ILogger<CraftableService> logger)
        {
            _clientFactory = clientFactory;
            _config = config;
            _logger = logger;
            _httpClient = GetHttpClient("StandardRequest");
        }

        public async Task<CraftableDTO> CreateCraftable(CraftableDTO craftable)
        {
            var resp = await _httpClient.PostAsync("api/craftables/", craftable, new JsonMediaTypeFormatter());
            if (resp.IsSuccessStatusCode)
            {
                return craftable;
            }

            return null;
        }

        public async Task<int> DeleteCraftable(int id)
        {
            var resp = await _httpClient.DeleteAsync("api/craftables/" + id);
            if (resp.IsSuccessStatusCode)
            {
                return id;
            }

            return 0;
        }

        public async Task<List<CraftableDTO>> GetAll()
        {
            try
            {
                var resp = await _httpClient.GetAsync("api/craftables");
                if (resp.IsSuccessStatusCode)
                {
                    var items = await resp.Content.ReadAsAsync<List<CraftableDTO>>();
                    return items;
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning("TaskCancelledException:\n" + ex);
            }

            return null;
        }

        public async Task<CraftableDTO> GetCraftable(int id)
        {
            try
            {
                var resp = await _httpClient.GetAsync("api/craftables/" + id);
                if (resp.IsSuccessStatusCode)
                {
                    var items = await resp.Content.ReadAsAsync<CraftableDTO>();
                    return items;
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning("" + ex);
            }

            return null;
        }

        public async Task<CraftableDTO> UpdateCraftable(CraftableDTO craftable)
        {
            var resp = await _httpClient.PutAsync("api/craftables/" + craftable.Id, craftable, new JsonMediaTypeFormatter());

            if (resp.IsSuccessStatusCode)
            {
                return craftable;
            }

            return null;
        }

        public async Task<CraftableDTO> UpdateCraftable(Craftable craftable)
        {
            var dto = new CraftableDTO
            {
                Effect = craftable.Effect,
                Form = craftable.Form,
                Id = craftable.Id,
                Materials = craftable.Materials,
                Name = craftable.Name,
                Requirement = craftable.Requirement
            };

            return await UpdateCraftable(dto);
        }

        private HttpClient GetHttpClient(string s)
        {
            var client = _clientFactory.CreateClient(s);
            client.BaseAddress = new Uri(_config["ItemsURL"]);
            return client;
        }
    }
}
