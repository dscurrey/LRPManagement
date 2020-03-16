﻿using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LRPManagement.Data.Players
{
    public class PlayerService : IPlayerService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<PlayerService> _logger;

        public PlayerService(IHttpClientFactory clientFactory, IConfiguration config, ILogger<PlayerService> logger)
        {
            _clientFactory = clientFactory;
            _config = config;
            _logger = logger;

            _httpClient = GetHttpClient("StandardRequest");
        }

        public async Task<List<PlayerDTO>> GetAll()
        {
            try
            {
                var resp = await _httpClient.GetAsync("api/players");
                if (resp.IsSuccessStatusCode)
                {
                    var players = await resp.Content.ReadAsAsync<List<PlayerDTO>>();
                    return players;
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning(""+ex);
            }

            return null;
        }

        public async Task<PlayerDTO> GetPlayer(int id)
        {
            try
            {
                var resp = await _httpClient.GetAsync("api/players/" + id);
                if (resp.IsSuccessStatusCode)
                {
                    var characters = await resp.Content.ReadAsAsync<PlayerDTO>();
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
            client.BaseAddress = new Uri(_config["PlayersURL"]);
            return client;
        }
    }
}