using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRP.Characters.Models;

namespace LRP.Characters.Data.Characters
{
    public interface ICharacterRepository
    {
        Task<List<Character>> GetAll();
        Task<Character> GetCharacter(int id);
        Task InsertCharacter(Character character);
        void DeleteCharacter(int id);
        void UpdateCharacter(Character character);
        Task Save();
    }
}
