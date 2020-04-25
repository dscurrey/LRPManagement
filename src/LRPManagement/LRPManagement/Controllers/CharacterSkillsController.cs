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

namespace LRPManagement.Controllers
{
    public class CharacterSkillsController : Controller
    {
        private readonly ISkillRepository _skillRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly ICharacterSkillRepository _characterSkillRepository;
        private readonly ICharacterService _characterService;

        public CharacterSkillsController(ISkillRepository skillRepository, ICharacterRepository characterRepository, ICharacterSkillRepository characterSkillRepository, ICharacterService characterService)
        {
            _characterRepository = characterRepository;
            _skillRepository = skillRepository;
            _characterSkillRepository = characterSkillRepository;
            _characterService = characterService;
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
                var skill = await _skillRepository.GetSkill(charSkill.SkillId);
                var character = await _characterRepository.GetCharacter(charSkill.CharacterId);

                if (skill != null && character != null)
                {
                    if (character.Xp - skill.XpCost >= 0)
                    {
                        // Subtract cost from character, add and save to link table
                        character.Xp -= skill.XpCost;
                        _characterSkillRepository.AddSkillToCharacter(charSkill.SkillId, charSkill.CharacterId);
                        await _characterSkillRepository.Save();

                        // Update Local and API
                        _characterRepository.UpdateCharacter(character);
                        await _characterRepository.Save();
                        await _characterService.UpdateCharacter(character);

                        return RedirectToAction(nameof(Index));
                    }
                }
                return View();
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
    }
}
