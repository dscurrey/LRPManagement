﻿using LRPManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRPManagement.Data.Players
{
    /// <summary>
    /// Repository for accessing and performing database operations with Players
    /// </summary>
    public interface IPlayerRepository
    {
        Task<List<Player>> GetAll();
        Task<Player> GetPlayer(int id);
        Task<Player> GetPlayerRef(int id);
        Task<Player> GetPlayerAccountRef(string id);
        Task<int> GetCount();
        void InsertPlayer(Player player);
        Task DeletePlayer(int id);
        Task AnonPlayer(int id);
        Task DeletePlayerRef(int id);
        void UpdatePlayer(Player player);
        Task Save();
    }
}