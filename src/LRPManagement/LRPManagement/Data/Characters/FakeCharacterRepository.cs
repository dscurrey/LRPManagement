using LRPManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Data.Characters
{
    public class FakeCharacterRepository : ICharacterRepository
    {
        private readonly List<Character> _characters;

        public FakeCharacterRepository(List<Character> characters)
        {
            _characters = characters;
        }

        public async Task<List<Character>> GetAll()
        {
            return await Task.FromResult(_characters.ToList());
        }

        public async Task<Character> GetCharacter(int id)
        {
            return await Task.FromResult(_characters.FirstOrDefault(c => c.Id == id));
        }

        public async Task<int> GetCount()
        {
            return await Task.FromResult(_characters.Count);
        }

        public async Task<Character> GetCharacterRef(int id)
        {
            return await Task.FromResult(_characters.FirstOrDefault(c => c.CharacterRef == id));
        }

        public void InsertCharacter(Character character)
        {
            _characters.Add(character);
        }

        public async Task DeleteCharacter(int id)
        {
            var character = _characters.FirstOrDefault(c => c.Id == id);
            await Task.FromResult(_characters.Remove(character));
        }

        public void UpdateCharacter(Character character)
        {
            var oldCharacter = _characters.FirstOrDefault(c => c.Id == character.Id);
            _characters.Remove(oldCharacter);
            _characters.Add(character);
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }
    }
}