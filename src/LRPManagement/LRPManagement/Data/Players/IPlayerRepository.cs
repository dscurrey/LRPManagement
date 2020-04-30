using LRPManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRPManagement.Data.Players
{
    public interface IPlayerRepository
    {
        Task<List<Player>> GetAll();
        Task<Player> GetPlayer(int id);
        Task<Player> GetPlayerRef(int id);
        Task<Player> GetPlayerAccountRef(string id);
        void InsertPlayer(Player player);
        Task DeletePlayer(int id);
        Task AnonPlayer(int id);
        Task DeletePlayerRef(int id);
        void UpdatePlayer(Player player);
        Task Save();
    }
}
