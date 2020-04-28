using LRPManagement.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LRPManagement.Services;

namespace LRPManagement.Data.Bonds
{
    public class BondService : IBondService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<BondService> _logger;
        private readonly ITokenBuilder _tokenBuilder;
        public HttpClient Client { get; set; }

        public BondService(IHttpClientFactory clientFactory, IConfiguration config, ILogger<BondService> logger, ITokenBuilder tokenBuilder)
        {
            _clientFactory = clientFactory;
            _config = config;
            _logger = logger;
            _tokenBuilder = tokenBuilder;
        }

        public async Task<Bond> Create(Bond bond)
        {
            var client = await GetHttpClient("StandardRequest");
            var resp = await client.PostAsync("api/bonds/", bond, new JsonMediaTypeFormatter());
            if (resp.IsSuccessStatusCode)
            {
                return bond;
            }

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var client = await GetHttpClient("StandardRequest");
            var resp = await client.DeleteAsync("api/bonds/" + id);
            if (resp.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<Bond> Get(int id)
        {
            try
            {
                var client = await GetHttpClient("StandardRequest");
                var resp = await client.GetAsync("api/bonds/" + id);
                if (resp.IsSuccessStatusCode)
                {
                    var bond = await resp.Content.ReadAsAsync<Bond>();
                    return bond;
                }
            }
            catch (TaskCanceledException e)
            {
                _logger.LogWarning("TaskCancelledException:\n" + e);
            }

            return null;
        }

        public async Task<List<Bond>> Get()
        {
            try
            {
                var client = await GetHttpClient("StandardRequest");
                var resp = await client.GetAsync("api/bonds/");
                if (resp.IsSuccessStatusCode)
                {
                    var bonds = await resp.Content.ReadAsAsync<List<Bond>>();
                    return bonds;
                }
            }
            catch (TaskCanceledException e)
            {
                Console.WriteLine(e);
                throw;
            }

            return null;
        }

        private async Task<HttpClient> GetHttpClient(string s)
        {
            if (Client != null && _clientFactory == null)
            {
                return Client;
            }
            var client = _clientFactory.CreateClient(s);

            var token = await _tokenBuilder.BuildToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            client.BaseAddress = new Uri(_config["ItemsURL"]);
            return client;
        }
    }
}
