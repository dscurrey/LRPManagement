using LRP.Characters.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRP.Characters.Data.Characters
{
    public interface ICharacterRepository
    {
        Task<List<Character>> GetAll();
        Task<Character> GetCharacter(int id);
        void InsertCharacter(Character character);
        void DeleteCharacter(int id);
        Task UpdateCharacter(Character character);
        Task Save();
    }
}
