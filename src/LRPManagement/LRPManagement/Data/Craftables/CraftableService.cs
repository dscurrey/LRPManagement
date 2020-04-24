using DTO;
using LRPManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public Task<CraftableDTO> CreateCraftable(CraftableDTO craftable)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteCraftable(int id)
        {
            throw new NotImplementedException();
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

        public Task<CraftableDTO> GetCraftable(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CraftableDTO> UpdateCraftable(CraftableDTO craftable)
        {
            throw new NotImplementedException();
        }

        public Task<CraftableDTO> UpdateCraftable(Craftable craftable)
        {
            throw new NotImplementedException();
        }

        private HttpClient GetHttpClient(string s)
        {
            var client = _clientFactory.CreateClient(s);
            client.BaseAddress = new Uri(_config["ItemsURL"]);
            return client;
        }
    }
}
