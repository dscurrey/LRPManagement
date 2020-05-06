using LRPManagement.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace LRPManagement.Data.Bonds
{
    /// <summary>
    /// Service for accessing and performing API operations with Bonds in the Items API
    /// </summary>
    public class BondService : IBondService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<BondService> _logger;
        public HttpClient Client { get; set; }

        public BondService(IHttpClientFactory clientFactory, IConfiguration config, ILogger<BondService> logger)
        {
            _clientFactory = clientFactory;
            _config = config;
            _logger = logger;
        }

        public async Task<Bond> Create(Bond bond)
        {
            var client = GetHttpClient("StandardRequest");
            var resp = await client.PostAsync("api/bonds/", bond, new JsonMediaTypeFormatter());
            if (resp.IsSuccessStatusCode) return bond;

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var client = GetHttpClient("StandardRequest");
            var resp = await client.DeleteAsync("api/bonds/" + id);
            if (resp.IsSuccessStatusCode) return true;

            return false;
        }

        public async Task<Bond> Get(int id)
        {
            try
            {
                var client = GetHttpClient("StandardRequest");
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
                var client = GetHttpClient("StandardRequest");
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

        private HttpClient GetHttpClient(string s)
        {
            if (Client != null && _clientFactory == null) return Client;
            var client = _clientFactory.CreateClient(s);
            client.BaseAddress = new Uri(_config["ItemsURL"]);
            return client;
        }
    }
}