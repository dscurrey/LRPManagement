using LRPManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRPManagement.Data.Characters
{
    /// <summary>
    /// Repository for accessing and performing database operations with Characters
    /// </summary>
    public interface ICharacterRepository
    {
        Task<List<Character>> GetAll();
        Task<Character> GetCharacter(int id);
        Task<int> GetCount();
        Task<Character> GetCharacterRef(int id);
        void InsertCharacter(Character character);
        Task DeleteCharacter(int id);
        void UpdateCharacter(Character character);
        Task Save();
    }
}