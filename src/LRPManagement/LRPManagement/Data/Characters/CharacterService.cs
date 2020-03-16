using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LRPManagement.Data.Characters
{
    public class CharacterService : ICharacterService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly ILogger<CharacterService> _logger;

        public CharacterService(IHttpClientFactory clientFactory, IConfiguration config, ILogger<CharacterService> logger)
        {
            _clientFactory = clientFactory;
            _config = config;
            _logger = logger;
            _httpClient = GetHttpClient("StandardRequest");
        }

        public async Task<List<CharacterDTO>> GetAll()
        {
            _logger.LogInformation("Sending API Request to Character Service (GetAll)");
            try
            {
                var resp = await _httpClient.GetAsync("api/characters");
                if (resp.IsSuccessStatusCode)
                {
                    var characters = await resp.Content.ReadAsAsync<List<CharacterDTO>>();
                    return characters;
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning("TaskCancelledException:\n"+ex);
            }

            return null;
        }

        public async Task<CharacterDTO> GetCharacter(int id)
        {
            try
            {
                var resp = await _httpClient.GetAsync("api/characters/" + id);
                if (resp.IsSuccessStatusCode)
                {
                    var characters = await resp.Content.ReadAsAsync<CharacterDTO>();
                    return characters;
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
            client.BaseAddress = new Uri(_config["CharactersURL"]);
            return client;
        }
    }
}
