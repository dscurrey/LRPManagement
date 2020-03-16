using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LRPManagement.Data.Players
{
    public class PlayerService : IPlayerService
    {
        private readonly HttpClient _httpClient;

        public PlayerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PlayerDTO>> GetAll()
        {
            var resp = await _httpClient.GetAsync("api/players");
            if (resp.IsSuccessStatusCode)
            {
                var players = await resp.Content.ReadAsAsync<List<PlayerDTO>>();
                return players;
            }
            return null;
        }

        public async Task<PlayerDTO> GetPlayer(int id)
        {
            var resp = await _httpClient.GetAsync("api/players/" + id);
            if (resp.IsSuccessStatusCode)
            {
                var characters = await resp.Content.ReadAsAsync<PlayerDTO>();
                return characters;
            }
            return null;
        }
    }
}
