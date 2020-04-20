using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using LRPManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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

        public async Task<CharacterDTO> UpdateCharacter(Character character)
        {
            var updCharacter = new CharacterDTO
            {
                Id = character.Id,
                IsRetired = character.IsRetired,
                IsActive = character.IsActive,
                Name = character.Name,
                PlayerId = character.PlayerId,
                Xp = character.Xp
            };
            return await UpdateCharacter(updCharacter);
        }

        public async Task<CharacterDTO> UpdateCharacter(CharacterDTO character)
        {
            try
            {
                var resp = await _httpClient.PutAsync("api/characters/" + character.Id, character, new JsonMediaTypeFormatter());
                if (resp.IsSuccessStatusCode)
                {
                    return character;
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning(""+ex);
            }

            return null;
        }

        public async Task<CharacterDTO> CreateCharacter(CharacterDTO character)
        {
            try
            {
                var resp = await _httpClient.PostAsync("api/characters/", character, new JsonMediaTypeFormatter());
                if (resp.IsSuccessStatusCode)
                {
                    return character;
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning("" + ex);
            }

            return null;
        }

        public async Task<int> DeleteCharacter(int id)
        {
            try
            {
                var resp = await _httpClient.DeleteAsync("api/characters/" + id);
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

        private HttpClient GetHttpClient(string s)
        {
            var client = _clientFactory.CreateClient(s);
            client.BaseAddress = new Uri(_config["CharactersURL"]);
            return client;
        }
    }
}
