using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Data.Characters
{
    public interface ICharacterService
    {
        Task<List<CharacterDTO>> GetAll();
        Task<CharacterDTO> GetCharacter(int id);
    }
}
