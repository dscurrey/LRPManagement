using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LRP.Characters.Data;
using LRP.Characters.Data.Characters;
using LRP.Characters.Models;
using Microsoft.Extensions.Logging;

namespace LRP.Characters.Controllers
{
    // TODO - Authorisation

    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterRepository _repository;
        private readonly ILogger<CharactersController> _logger;

        public CharactersController(ICharacterRepository repository, ILogger<CharactersController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET: api/Characters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Character>>> GetCharacter()
        {
            return await _repository.GetAll();
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacter(int id)
        {
            var character = await _repository.GetCharacter(id);

            if (character == null)
            {
                return NotFound();
            }

            return character;
        }

        // PUT: api/Characters/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, Character character)
        {
            if (id != character.Id)
            {
                return BadRequest();
            }

            _repository.UpdateCharacter(character);

            try
            {
                await _repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await CharacterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError("Error occured when saving updated player.");
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Characters
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(Character character)
        {
            // TODO - At Char Creation, populate player id using OAUTH/PlayerRepo?
            await _repository.InsertCharacter(character);
            await _repository.Save();

            return CreatedAtAction("GetCharacter", new { id = character.Id }, character);
        }

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Character>> DeleteCharacter(int id)
        {
            if (!await CharacterExists(id))
            {
                return NotFound();
            }

            var character = await _repository.GetCharacter(id);

            _repository.DeleteCharacter(id);
            await _repository.Save();

            return character;
        }

        private async Task<bool> CharacterExists(int id)
        {
            var all = await _repository.GetAll();
            return all.Any(p => p.Id == id);
        }
    }
}
