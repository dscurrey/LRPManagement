using LRP.Items.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRP.Items.Data.Craftables
{
    public interface ICraftableRepository
    {
        /// <summary>
        /// Gets all craftable (item) entities in database
        /// </summary>
        /// <returns>A list of craftables</returns>
        Task<List<Craftable>> GetAll();

        /// <summary>
        /// Gets the craftable associated with a specific ID
        /// </summary>
        /// <param name="id">The unique ID of the craftable</param>
        /// <returns>The craftable with matching ID</returns>
        Task<Craftable> GetCraftable(int id);

        /// <summary>
        /// Inserts a craftable into the database
        /// </summary>
        /// <param name="craftable">The craftable to be inserted</param>
        void InsertCraftable(Craftable craftable);

        /// <summary>
        /// Deletes a craftable
        /// </summary>
        /// <param name="id">The ID of the craftable to be deleted</param>
        /// <returns>Task</returns>
        Task DeleteCraftable(int id);

        /// <summary>
        /// Updates a craftable
        /// </summary>
        /// <param name="craftable">The craftable to be updated</param>
        void UpdateCraftable(Craftable craftable);

        /// <summary>
        /// Saves changes made to DB context
        /// </summary>
        /// <returns>Task</returns>
        Task Save();
    }
}