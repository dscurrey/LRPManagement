using LRP.Skills.Models;
using LRPManagement.Data.CharacterSkills;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRP.Skills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterSkillsController : ControllerBase
    {
        private readonly ICharacterSkillRepository _repository;

        public CharacterSkillsController(ICharacterSkillRepository repository)
        {
            _repository = repository;
        }

        // GET: api/CharacterSkills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterSkill>>> GetCharacterSkills()
        {
            return await _repository.Get();
        }

        // GET: api/CharacterSkills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterSkill>> GetCharacterSkill(int id)
        {
            var characterSkill = await _repository.Get(id);

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
            _repository.Insert(characterSkill);
            await _repository.Save();

            return CreatedAtAction("GetCharacterSkill", new { id = characterSkill.Id }, characterSkill);
        }

        // DELETE: api/CharacterSkills/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CharacterSkill>> DeleteCharacterSkill(int id)
        {
            var characterSkill = await _repository.Get(id);
            if (characterSkill == null)
            {
                return NotFound();
            }

            await _repository.Delete(id);
            await _repository.Save();

            return characterSkill;
        }
    }
}
