using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRP.Players.Models;

namespace LRP.Players.Data.Players
{
    public interface IPlayerRepository
    {
        Task<List<Player>> GetAll();
        Task<Player> GetPlayer(int id);
        Task InsertPlayer(Player player);
        void DeletePlayer(int id);
        void UpdatePlayer(Player player);
        Task Save();
    }
}
