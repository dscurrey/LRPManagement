﻿using DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace LRPManagement.Data.Players
{
    public class PlayerService : IPlayerService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<PlayerService> _logger;

        public HttpClient Client { get; set; }

        public PlayerService(IHttpClientFactory clientFactory, IConfiguration config, ILogger<PlayerService> logger)
        {
            _clientFactory = clientFactory;
            _config = config;
            _logger = logger;
        }

        public async Task<List<PlayerDTO>> GetAll()
        {
            try
            {
                var client = GetHttpClient("StandardRequest");
                var resp = await client.GetAsync("api/players");
                if (resp.IsSuccessStatusCode)
                {
                    var players = await resp.Content.ReadAsAsync<List<PlayerDTO>>();
                    return players;
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning("" + ex);
            }

            return null;
        }

        public async Task<PlayerDTO> GetPlayer(int id)
        {
            try
            {
                var client = GetHttpClient("StandardRequest");
                var resp = await client.GetAsync("api/players/" + id);
                if (resp.IsSuccessStatusCode)
                {
                    var characters = await resp.Content.ReadAsAsync<PlayerDTO>();
                    return characters;
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning("" + ex);
            }

            return null;
        }

        public async Task<PlayerDTO> UpdatePlayer(PlayerDTO player)
        {
            try
            {
                var client = GetHttpClient("StandardRequest");
                var resp = await client.PutAsync("api/players/" + player.Id, player, new JsonMediaTypeFormatter());
                if (resp.IsSuccessStatusCode) return player;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning("" + ex);
            }

            return null;
        }

        public async Task<PlayerDTO> CreatePlayer(PlayerDTO player)
        {
            try
            {
                var client = GetHttpClient("StandardRequest");
                var resp = await client.PostAsync("api/players/", player, new JsonMediaTypeFormatter());
                if (resp.IsSuccessStatusCode) return player;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning("" + ex);
            }

            return null;
        }

        public async Task<int> DeletePlayer(int id)
        {
            try
            {
                var client = GetHttpClient("StandardRequest");
                var resp = await client.DeleteAsync("api/players/" + id);
                if (resp.IsSuccessStatusCode) return id;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning("" + ex);
            }

            return 0;
        }

        private HttpClient GetHttpClient(string s)
        {
            if (Client != null && _clientFactory == null) return Client;

            var client = _clientFactory.CreateClient(s);
            client.BaseAddress = new Uri(_config["PlayersURL"]);
            return client;
        }
    }
}