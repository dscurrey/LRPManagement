using DTO;
using LRPManagement.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LRPManagement.Controllers;
using LRPManagement.Services;
using Microsoft.AspNetCore.Http;

namespace LRPManagement.Data.Craftables
{
    public class CraftableService : ICraftableService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<CraftableService> _logger;
        private readonly ITokenBuilder _tokenBuilder;

        public HttpClient Client { get; set; }

        public CraftableService(IHttpClientFactory clientFactory, IConfiguration config, ILogger<CraftableService> logger, ITokenBuilder tokenBuilder)
        {
            _clientFactory = clientFactory;
            _config = config;
            _logger = logger;
            _tokenBuilder = tokenBuilder;
        }

        public async Task<CraftableDTO> CreateCraftable(CraftableDTO craftable)
        {
            var client = await GetHttpClient("StandardRequest");
            var resp = await client.PostAsync("api/craftables/", craftable, new JsonMediaTypeFormatter());
            if (resp.IsSuccessStatusCode)
            {
                return craftable;
            }

            return null;
        }

        public async Task<int> DeleteCraftable(int id)
        {
            var client = await GetHttpClient("StandardRequest");
            var resp = await client.DeleteAsync("api/craftables/" + id);
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
                var client = await GetHttpClient("StandardRequest");
                var resp = await client.GetAsync("api/craftables");
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
                var client = await GetHttpClient("StandardRequest");
                var resp = await client.GetAsync("api/craftables/" + id);
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
            var client = await GetHttpClient("StandardRequest");
            var resp = await client.PutAsync("api/craftables/" + craftable.Id, craftable, new JsonMediaTypeFormatter());

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

        private async Task<HttpClient> GetHttpClient(string s)
        {
            if (Client != null && _clientFactory == null)
            {
                return Client;
            }

            var client = _clientFactory.CreateClient(s);
            client.BaseAddress = new Uri(_config["ItemsURL"]);

            var token = await _tokenBuilder.BuildToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }
    }
}
