using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Data.Players
{
    public interface IPlayerService
    {
        Task<List<PlayerDTO>> GetAll();
        Task<PlayerDTO> GetPlayer(int id);
        Task<PlayerDTO> UpdatePlayer(PlayerDTO player);
        Task<PlayerDTO> CreatePlayer(PlayerDTO player);
        Task<int> DeletePlayer(int id);
    }
}
