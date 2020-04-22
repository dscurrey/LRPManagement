using System.Collections.Generic;
using System.Threading.Tasks;
using LRPManagement.Models;

namespace LRPManagement.Data.Characters
{
    public interface ICharacterRepository
    {
        Task<List<Character>> GetAll();
        Task<Character> GetCharacter(int id);
        Task<Character> GetCharacterRef(int id);
        void InsertCharacter(Character character);
        Task DeleteCharacter(int id);
        void UpdateCharacter(Character character);
        Task Save();
    }
}
