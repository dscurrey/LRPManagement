using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LRPManagement.Data.Characters
{
    public class CharacterService : ICharacterService
    {
        private readonly HttpClient _httpClient;

        public CharacterService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CharacterDTO>> GetAll()
        {
            var resp = await _httpClient.GetAsync("api/characters");
            if (resp.IsSuccessStatusCode)
            {
                var characters = await resp.Content.ReadAsAsync<List<CharacterDTO>>();
                return characters;
            }
            return null;
        }

        public Task<CharacterDTO> GetCharacter(int id)
        {
            throw new NotImplementedException();
        }
    }
}
