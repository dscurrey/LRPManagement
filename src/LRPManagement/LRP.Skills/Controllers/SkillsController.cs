using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LRP.Skills.Data;
using LRP.Skills.Data.Skills;
using LRP.Skills.Models;
using Microsoft.Extensions.Logging;

namespace LRP.Skills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillRepository _repository;
        private readonly ILogger<SkillsController> _logger;

        public SkillsController(ISkillRepository repository, ILogger<SkillsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET: api/Skills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkill()
        {
            return await _repository.GetAll();
        }

        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetSkill(int id)
        {
            var skill = await _repository.GetSkill(id);

            if (skill == null)
            {
                return NotFound();
            }

            return skill;
        }

        // PUT: api/Skills/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkill(int id, Skill skill)
        {
            if (id != skill.Id)
            {
                return BadRequest();
            }

            _repository.UpdateSkill(skill);

            try
            {
                _logger.LogInformation("Saving updated skill");
                await _repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SkillExists(id))
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

        // POST: api/Skills
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Skill>> PostSkill(Skill skill)
        {
            _repository.InsertSkill(skill);
            await _repository.Save();

            return CreatedAtAction("GetSkill", new { id = skill.Id }, skill);
        }

        // DELETE: api/Skills/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Skill>> DeleteSkill(int id)
        {
            if (!await SkillExists(id))
            {
                return NotFound();
            }

            var player = await _repository.GetSkill(id);

            _repository.DeleteSkill(id);
            await _repository.Save();

            return player;
        }

        private async Task<bool> SkillExists(int id)
        {
            var all = await _repository.GetAll();
            return all.Any(p => p.Id == id);
        }
    }
}
