using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LRPManagement.Data;
using LRPManagement.Data.Characters;
using LRPManagement.Data.CharacterSkills;
using LRPManagement.Data.Skills;
using LRPManagement.Models;
using Polly.CircuitBreaker;

namespace LRPManagement.Controllers
{
    public class CharacterSkillsController : Controller
    {
        private readonly ISkillRepository _skillRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly ICharacterSkillRepository _characterSkillRepository;
        private readonly ICharacterService _characterService;
        private readonly ICharacterSkillService _characterSkillService;

        public CharacterSkillsController(ISkillRepository skillRepository, ICharacterRepository characterRepository, ICharacterSkillRepository characterSkillRepository, ICharacterService characterService, ICharacterSkillService characterSkillService)
        {
            _characterRepository = characterRepository;
            _skillRepository = skillRepository;
            _characterSkillRepository = characterSkillRepository;
            _characterService = characterService;
            _characterSkillService = characterSkillService;
        }

        // GET: CharacterSkills
        public async Task<IActionResult> Index()
        {
            return View(await _characterSkillRepository.Get());
        }

        // GET: CharacterSkills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characterSkill = await _characterSkillRepository.Get(id.Value);

            if (characterSkill == null)
            {
                return NotFound();
            }

            return View(characterSkill);
        }

        // GET: CharacterSkills/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CharacterId"] = "";
            ViewData["SkillId"] = "";

            var characters = await _characterRepository.GetAll();
            var skills = await _skillRepository.GetAll();

            if (characters.Any())
            {
                var charList = characters.Select
                (
                    s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    }
                );
                ViewData["CharacterId"] = charList;
            }

            if (skills.Any())
            {
                var skillList = skills.Select
                (
                    s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    }
                );
                ViewData["SkillId"] = skillList;
            }

            return View();
        }

        // POST: CharacterSkills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CharacterId,SkillId")] CharacterSkill charSkill)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resp = await _characterSkillService.Create(charSkill);
                    if (resp == null)
                    {
                        return View(charSkill);
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (BrokenCircuitException)
                {
                    HandleBrokenCircuit();
                    return View();
                }
            }

            ViewData["CharacterId"] = "";
            ViewData["SkillId"] = "";

            var characters = await _characterRepository.GetAll();
            var skills = await _skillRepository.GetAll();

            if (characters.Any())
            {
                var charList = characters.Select
                (
                    s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    }
                );
                ViewData["CharacterId"] = charList;
            }

            if (skills.Any())
            {
                var skillList = skills.Select
                (
                    s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    }
                );
                ViewData["SkillId"] = skillList;
            }
            return View(charSkill);
        }

        // GET: CharacterSkills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characterSkill = await _characterSkillRepository.Get(id.Value);

            if (characterSkill == null)
            {
                return NotFound();
            }

            return View(characterSkill);
        }

        // POST: CharacterSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var characterSkill = await _characterSkillRepository.Get(id);
            await _characterSkillRepository.Delete(id);
            await _characterSkillRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CharSkillExists(int skillId, int charId)
        {
            var charSkill = await _characterSkillRepository.GetMatch(charId, skillId);
            return charSkill != null;
        }

        private void HandleBrokenCircuit()
        {
            ViewBag.CharacterSkillsError = "Character Service is Currently Unavailable";
        }
    }
}
