using LRP.Players.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRP.Players.Data.Players
{
    public interface IPlayerRepository
    {
        Task<List<Player>> GetAll();
        Task<Player> GetPlayer(int id);
        void InsertPlayer(Player player);
        void DeletePlayer(int id);
        void UpdatePlayer(Player player);
        Task Save();
    }
}
