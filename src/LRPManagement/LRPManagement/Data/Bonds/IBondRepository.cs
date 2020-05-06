using LRPManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRPManagement.Data.Bonds
{
    /// <summary>
    /// Repository for accessing and performing database operations with Bonds (Character - Item link entity)
    /// </summary>
    public interface IBondRepository
    {
        Task<List<Bond>> GetAll();
        Task<Bond> Get(int id);
        Task<List<Bond>> GetForItem(int itemId);
        Task<List<Bond>> GetForPlayer(int playerId);

        /// <summary>
        /// Returns the first encountered bond between the chosen character and item
        /// Each character/item should only be bonded once
        /// </summary>
        /// <param name="charId">ID of selected Character</param>
        /// <param name="itemId">ID of selected item</param>
        /// <returns></returns>
        Task<Bond> GetMatch(int charId, int itemId);

        void Insert(Bond bond);
        Task Delete(int id);
        Task Save();
    }
}