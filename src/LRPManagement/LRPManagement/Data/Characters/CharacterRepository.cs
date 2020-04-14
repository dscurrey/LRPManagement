using LRPManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LRPManagement.Data.Characters
{
    public class CharacterRepository : ICharacterRepository
    {

        private readonly LrpDbContext _context;

        public CharacterRepository(LrpDbContext context)
        {
            _context = context;
        }

        public void DeleteCharacter(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Character>> GetAll()
        {
            return await _context.Characters.ToListAsync();
        }

        public async Task<Character> GetCharacter(int id)
        {
            return await _context.Characters.Include(c => c.Bond).FirstOrDefaultAsync(c => c.Id == id); ;
        }

        public async Task<Character> GetCharacterRef(int id)
        {
            return await _context.Characters.FirstOrDefaultAsync(c => c.CharacterRef == id);
        }

        public async void InsertCharacter(Character character)
        {
            await _context.Characters.AddAsync(character);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateCharacter(Character character)
        {
            throw new NotImplementedException();
        }
    }
}
