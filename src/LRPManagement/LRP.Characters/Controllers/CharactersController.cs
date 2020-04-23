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
using DTO;

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
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> GetCharacter()
        {
            var chars = await _repository.GetAll();
            return Ok(chars.Select
            (
                c => new CharacterDTO
                {
                    Id = c.Id,
                    IsActive = c.IsActive,
                    IsRetired = c.IsRetired,
                    Name = c.Name,
                    PlayerId = c.PlayerId,
                    Xp = c.Xp
                }
            ).ToList());
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDTO>> GetCharacter(int id)
        {
            var character = await _repository.GetCharacter(id);

            if (character == null)
            {
                return NotFound();
            }

            CharacterDTO charDto = new CharacterDTO
            {
                Id = character.Id,
                IsActive = character.IsActive,
                IsRetired = character.IsRetired,
                Name = character.Name,
                PlayerId = character.PlayerId
            };

            return Ok(charDto);
        }

        // PUT: api/Characters/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, CharacterDTO character)
        {
            if (id != character.Id)
            {
                return BadRequest();
            }

            var updChar = new Character
            {
                PlayerId = character.PlayerId,
                IsActive = character.IsActive,
                IsRetired = character.IsRetired,
                Name = character.Name,
                Xp = character.Xp
            };

            if (!await CharacterExists(id))
            {
                return NotFound();
            }

            _repository.UpdateCharacter(updChar);

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
        public async Task<ActionResult<Character>> PostCharacter([FromBody] CharacterDTO character)
        {
            if (String.IsNullOrEmpty(character.Name))
            {
                return BadRequest("Name cannot be empty.");
            }

            var newCharacter = new Character
            {
                PlayerId = character.PlayerId,
                IsActive = character.IsActive,
                IsRetired = character.IsRetired,
                Name = character.Name
            };

            _repository.InsertCharacter(newCharacter);
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
