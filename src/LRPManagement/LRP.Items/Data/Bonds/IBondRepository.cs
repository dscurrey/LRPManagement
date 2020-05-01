using LRP.Items.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRP.Items.Data.Bonds
{
    public interface IBondRepository
    {
        /// <summary>
        /// Gets all bonds in database
        /// </summary>
        /// <returns>A list of bonds</returns>
        Task<List<Bond>> GetAll();

        /// <summary>
        /// Gets the bond associated with the ID
        /// </summary>
        /// <param name="id">Unique ID of the bond</param>
        /// <returns>Matching bond</returns>
        Task<Bond> Get(int id);

        /// <summary>
        /// Gets a list of bonds which contain a specified item/craftable
        /// </summary>
        /// <param name="itemId">ID of the chosen item/craftable</param>
        /// <returns>A matching list of bonds</returns>
        Task<List<Bond>> GetForItem(int itemId);

        /// <summary>
        /// Gets a list of bonds which belong to a specified character
        /// </summary>
        /// <param name="charId">The ID of the character</param>
        /// <returns>A list of bonds</returns>
        Task<List<Bond>> GetForCharacter(int charId);

        /// <summary>
        /// Inserts a bond into the database
        /// </summary>
        /// <param name="bond">New Bond to be inserted</param>
        void Insert(Bond bond);

        /// <summary>
        /// Deletes a bond
        /// </summary>
        /// <param name="id">ID of the bond to be deleted</param>
        /// <returns>Task</returns>
        Task Delete(int id);

        /// <summary>
        /// Saves changes made to DB context
        /// </summary>
        /// <returns>Task</returns>
        Task Save();
    }
}
