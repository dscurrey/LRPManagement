using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LRP.Skills.Data;
using LRP.Skills.Models;

namespace LRP.Skills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterSkillsController : ControllerBase
    {
        private readonly SkillDbContext _context;

        public CharacterSkillsController(SkillDbContext context)
        {
            _context = context;
        }

        // GET: api/CharacterSkills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterSkill>>> GetCharacterSkills()
        {
            return await _context.CharacterSkills.ToListAsync();
        }

        // GET: api/CharacterSkills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterSkill>> GetCharacterSkill(int id)
        {
            var characterSkill = await _context.CharacterSkills.FindAsync(id);

            if (characterSkill == null)
            {
                return NotFound();
            }

            return characterSkill;
        }

        // POST: api/CharacterSkills
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CharacterSkill>> PostCharacterSkill(CharacterSkill characterSkill)
        {
            _context.CharacterSkills.Add(characterSkill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharacterSkill", new { id = characterSkill.Id }, characterSkill);
        }

        // DELETE: api/CharacterSkills/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CharacterSkill>> DeleteCharacterSkill(int id)
        {
            var characterSkill = await _context.CharacterSkills.FindAsync(id);
            if (characterSkill == null)
            {
                return NotFound();
            }

            _context.CharacterSkills.Remove(characterSkill);
            await _context.SaveChangesAsync();

            return characterSkill;
        }

        private bool CharacterSkillExists(int id)
        {
            return _context.CharacterSkills.Any(e => e.Id == id);
        }
    }
}
