using LRP.Characters.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRP.Characters.Data.Characters
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly CharacterDbContext _context;

        public CharacterRepository(CharacterDbContext context)
        {
            _context = context;
        }

        public async Task<List<Character>> GetAll()
        {
            return await _context.Character.ToListAsync();
        }

        public async Task<Character> GetCharacter(int id)
        {
            return await _context.Character.FirstOrDefaultAsync(c => c.Id == id);
        }

        public void InsertCharacter(Character character)
        {
            _context.Character.Add(character);
        }

        public async void DeleteCharacter(int id)
        {
            var character = await _context.Character.FirstOrDefaultAsync(c => c.Id == id);
            character.IsRetired = true;
            character.IsActive = false;
            _context.Character.Update(character);
        }

        public async Task UpdateCharacter(Character character)
        {
            var characterOrig = await _context.Character.FirstOrDefaultAsync(c => c.Id == character.Id);
            _context.Entry(characterOrig).CurrentValues.SetValues(character);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}