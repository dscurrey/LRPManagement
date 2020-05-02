using LRP.Characters.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRP.Characters.Data.Characters
{
    public interface ICharacterRepository
    {
        /// <summary>
        /// Gets all character entities in database
        /// </summary>
        /// <returns>A list of Characters</returns>
        Task<List<Character>> GetAll();

        /// <summary>
        ///Gets the character associated with a specific ID
        /// </summary>
        /// <param name="id">The unique ID of the character</param>
        /// <returns>The character with matching ID</returns>
        Task<Character> GetCharacter(int id);

        /// <summary>
        /// Inserts a character into the database
        /// </summary>
        /// <param name="character">The character to be inserted</param>
        void InsertCharacter(Character character);

        /// <summary>
        /// Deletes a character from the database
        /// </summary>
        /// <param name="id">ID for the character to be deleted</param>
        void DeleteCharacter(int id);

        /// <summary>
        /// Updates a character in the database
        /// </summary>
        /// <param name="character">The updated character</param>
        /// <returns>Task</returns>
        Task UpdateCharacter(Character character);

        /// <summary>
        /// Saves changes to the DB context
        /// </summary>
        /// <returns>Task</returns>
        Task Save();
    }
}