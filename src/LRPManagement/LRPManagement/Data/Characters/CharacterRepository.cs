using LRPManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Data.Characters
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly LrpDbContext _context;

        public CharacterRepository(LrpDbContext context)
        {
            _context = context;
        }

        public async Task DeleteCharacter(int id)
        {
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            _context.Characters.Remove(character);
        }

        public async Task<List<Character>> GetAll()
        {
            return await _context.Characters.ToListAsync();
        }

        public async Task<Character> GetCharacter(int id)
        {
            return await _context.Characters.Include(c => c.Bond).ThenInclude(b => b.Item).Include(c => c.CharacterSkills).ThenInclude(c => c.Skill).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Character> GetCharacterRef(int id)
        {
            return await _context.Characters.FirstOrDefaultAsync(c => c.CharacterRef == id);
        }

        public void InsertCharacter(Character character)
        {
            _context.Characters.Add(character);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateCharacter(Character character)
        {
            var dbChar = _context.Characters.First(c => c.Id == character.Id);
            if (dbChar != null)
            {
                _context.Entry(dbChar).CurrentValues.SetValues(character);
            }
            else
            {
                _context.Characters.Update(character);
            }
        }
    }
}
