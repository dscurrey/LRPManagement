using DTO;
using LRPManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRPManagement.Data.Characters
{
    public interface ICharacterService
    {
        Task<List<CharacterDTO>> GetAll();
        Task<CharacterDTO> GetCharacter(int id);
        Task<CharacterDTO> UpdateCharacter(CharacterDTO character);
        Task<CharacterDTO> UpdateCharacter(Character character);
        Task<CharacterDTO> CreateCharacter(CharacterDTO character);
        Task<int> DeleteCharacter(int id);
    }
}