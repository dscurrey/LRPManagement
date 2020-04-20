using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
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
        public async Task<ActionResult<IEnumerable<SkillDTO>>> GetSkill()
        {
            var skills =  await _repository.GetAll();
            return Ok(skills.Select
            (
                s => new SkillDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    XpCost = s.XpCost
                }
            ).ToList());
        }

        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillDTO>> GetSkill(int id)
        {
            var skill = await _repository.GetSkill(id);

            if (skill == null)
            {
                return NotFound();
            }

            var skillDto = new SkillDTO
            {
                Id = skill.Id,
                Name = skill.Name,
                XpCost = skill.XpCost
            };

            return Ok(skillDto);
        }

        // PUT: api/Skills/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkill(int id, SkillDTO skill)
        {
            if (id != skill.Id)
            {
                return BadRequest();
            }

            var updSkill = new Skill
            {
                Id = skill.Id,
                Name = skill.Name,
                XpCost = skill.XpCost
            };

            if (!await SkillExists(id))
            {
                return NotFound();
            }

            _repository.UpdateSkill(updSkill);

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
        public async Task<ActionResult<Skill>> PostSkill(SkillDTO skill)
        {
            if (String.IsNullOrEmpty(skill.Name))
            {
                return BadRequest("Name cannot be empty");
            }

            var newSkill = new Skill
            {
                Id = skill.Id,
                Name = skill.Name,
                XpCost = skill.XpCost
            };

            _repository.InsertSkill(newSkill);
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
