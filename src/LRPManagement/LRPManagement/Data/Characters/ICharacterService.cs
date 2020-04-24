using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRPManagement.Models;
using Microsoft.AspNetCore.Mvc;

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
