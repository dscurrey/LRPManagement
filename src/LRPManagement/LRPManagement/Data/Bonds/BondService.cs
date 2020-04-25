using LRPManagement.Models;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using LRPManagement.Data.Characters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LRPManagement.Data.Bonds
{
    public class BondService : IBondService
    {
        private IHttpClientFactory _clientFactory;
        private IConfiguration _config;
        private ILogger<BondService> _logger;
        private HttpClient _client;

        public BondService(IHttpClientFactory clientFactory, IConfiguration config, ILogger<BondService> logger)
        {
            _clientFactory = clientFactory;
            _config = config;
            _logger = logger;
            _client = GetHttpClient("StandardRequest");
        }

        public async Task<Bond> Create(Bond bond)
        {
            var resp = await _client.PostAsync("api/bonds/", bond, new JsonMediaTypeFormatter());
            if (resp.IsSuccessStatusCode)
            {
                return bond;
            }

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var resp = await _client.DeleteAsync("api/bonds/" + id);
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
                var resp = await _client.GetAsync("api/bonds/" + id);
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
                var resp = await _client.GetAsync("api/bonds/");
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
            var client = _clientFactory.CreateClient(s);
            client.BaseAddress = new Uri(_config["ItemsURL"]);
            return client;
        }
    }
}
