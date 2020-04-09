using LRP.Characters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRP.Characters.Data.Characters
{
    public class FakeCharacterRepository : ICharacterRepository
    {
        private readonly List<Character> _characters;

        public FakeCharacterRepository(List<Character> characters)
        {
            _characters = characters;
        }

        public void DeleteCharacter(int id)
        {
            var character = _characters.Find(c => c.Id == id);
            _characters.Remove(character);
        }

        public Task<List<Character>> GetAll()
        {
            return Task.FromResult(_characters.ToList());
        }

        public Task<Character> GetCharacter(int id)
        {
            return Task.FromResult(_characters.FirstOrDefault(c => c.Id == id));
        }

        public void InsertCharacter(Character character)
        {
            _characters.Add(character);
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }

        public void UpdateCharacter(Character character)
        {
            var tgt = _characters.Find(s => s.Id == character.Id);
            _characters.Remove(tgt);
            _characters.Add(character);
        }
    }
}
