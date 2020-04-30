using LRP.Players.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;

namespace LRP.Players.Data.Players
{
    public interface IPlayerRepository
    {
        Task<List<Player>> GetAll();
        Task<Player> GetPlayer(int id);
        void InsertPlayer(Player player);
        Task DeletePlayer(int id);
        void UpdatePlayer(Player player);
        void UpdatePlayer(PlayerDTO player);
        Task Save();
    }
}
