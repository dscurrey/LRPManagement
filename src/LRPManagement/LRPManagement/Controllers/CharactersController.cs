using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DTO;
using LRPManagement.Data;
using LRPManagement.Data.Characters;
using LRPManagement.Data.CharacterSkills;
using LRPManagement.Data.Players;
using LRPManagement.Data.Skills;
using LRPManagement.Models;
using LRPManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Polly.CircuitBreaker;

namespace LRPManagement.Controllers
{
    [Authorize]
    public class CharactersController : Controller
    {
        private readonly ICharacterService _characterService;
        private readonly ICharacterRepository _characterRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly ICharacterSkillRepository _charSkillRepository;

        public CharactersController(ICharacterService characterService, ICharacterRepository characterRepository, IPlayerRepository playerRepository,
            ISkillRepository skillRepository, ICharacterSkillRepository charSkillRepository)
        {
            _characterService = characterService;
            _characterRepository = characterRepository;
            _playerRepository = playerRepository;
            _skillRepository = skillRepository;
            _charSkillRepository = charSkillRepository;
        }

        // GET: Characters
        [Authorize(Policy = "StaffOnly")]
        public async Task<IActionResult> Index()
        {
            TempData["CharInoperativeMsg"] = "";

            try
            {
                var characters = await _characterService.GetAll();
                if (characters != null)
                {
                    foreach (var character in characters)
                    {
                        if (await _characterRepository.GetCharacterRef(character.Id) != null || await _playerRepository.GetPlayer(character.PlayerId) == null)
                        {
                            continue;
                        }

                        var newChar = new Character
                        {
                            Name = character.Name,
                            IsActive = character.IsActive,
                            IsRetired = character.IsRetired,
                            CharacterRef = character.Id,
                            PlayerId = character.PlayerId,
                            Xp = character.Xp
                        };
                        _characterRepository.InsertCharacter(newChar);
                        await _characterRepository.Save();
                    }
                }
            }
            catch (BrokenCircuitException e)
            {
                Console.WriteLine(e);
            }

            try
            {
                var characters = await _characterRepository.GetAll();
                var charList = characters.Select
                (
                    c => new CharacterDTO
                    {
                        Id = c.Id,
                        IsRetired = c.IsRetired,
                        IsActive = c.IsActive,
                        Name = c.Name,
                        PlayerId = c.PlayerId
                    }
                );

                return View(charList);
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return View();
        }

        // GET: Characters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            TempData["CharInoperativeMsg"] = "";
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var character = await _characterRepository.GetCharacter(id.Value);
                if (character == null)
                {
                    return NotFound();
                }

                var player = await _playerRepository.GetPlayerAccountRef(User.Identity.Name);
                if (User.IsInRole("Admin") || User.IsInRole("Referee") || character.PlayerId == player.Id)
                {
                    var skills = new List<Skill>();
                    foreach (var charSkill in character.CharacterSkills)
                    {
                        skills.Add(charSkill.Skill);
                    }

                    var charView = new CharacterDetailsViewModel
                    {
                        Id = character.Id,
                        IsActive = character.IsActive,
                        Name = character.Name,
                        PlayerId = character.PlayerId,
                        Skills = skills,
                        Xp = character.Xp
                    };

                    return View(charView);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }
            return View();
        }

        // GET: Characters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlayerId,Name,IsActive,IsRetired")] CharacterDTO characterDto)
        {
            TempData["CharInoperativeMsg"] = "";

            try
            {
                var player = await _playerRepository.GetPlayerAccountRef(User.Identity.Name);
                if (player == null)
                {
                    return View();
                }

                characterDto.PlayerId = player.Id;
                var resp = await _characterService.CreateCharacter(characterDto);
                if (resp == null)
                {
                    // Unsuccessful/Error
                    return View(characterDto);
                }
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Characters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var character = await _characterService.GetCharacter(id.Value);
                if (character != null)
                {
                    return View(character);
                }
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return NotFound();
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlayerId,Name,IsActive,IsRetired")] CharacterDTO characterDTO)
        {
            if (id != characterDTO.Id)
            {
                return NotFound();
            }

            try
            {
                var resp = await _characterService.UpdateCharacter(characterDTO);
                if (resp != null)
                {
                    return RedirectToAction(nameof(Index));
                }

                var updChar = new Character
                {
                    IsActive = characterDTO.IsActive,
                    IsRetired = characterDTO.IsRetired,
                    Name = characterDTO.Name,
                    PlayerId = characterDTO.PlayerId
                };

                _characterRepository.UpdateCharacter(updChar);
                await _characterRepository.Save();
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return View(characterDTO);
        }

        // GET: Characters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var character = await _characterService.GetCharacter(id.Value);
                if (character != null)
                {
                    return View(character);
                }
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return NotFound();
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TempData["CharInoperativeMsg"] = "";
            try
            {
                await _characterService.DeleteCharacter(id);

                _characterRepository.DeleteCharacter(id);
                await _characterRepository.Save();
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddSkill(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var character = await _characterRepository.GetCharacter(id.Value);
                if (character != null)
                {
                    var skills = await _skillRepository.GetAll();
                    if (skills.Any())
                    {
                        var list = skills.Select
                        (
                            s => new SelectListItem
                            {
                                Value = s.Id.ToString(),
                                Text = s.Name
                            }
                        );
                        ViewBag.Skills = list;
                    }

                    var viewModel = new CharacterSkillViewModel
                    {
                        CharId = character.Id
                    };

                    return View(viewModel);
                }
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return NotFound();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSkill(int id, [Bind("CharId, SkillId")] CharacterSkillViewModel charSkill)
        {
            var skill = await _skillRepository.GetSkill(charSkill.SkillId);
            var character = await _characterRepository.GetCharacter(charSkill.CharId);

            if (skill != null && character != null)
            {
                if (character.Xp - skill.XpCost >= 0)
                {
                    // Subtract cost from character, add and save to link table
                    character.Xp -= skill.XpCost;
                    _charSkillRepository.AddSkillToCharacter(charSkill.SkillId, id);
                    await _charSkillRepository.Save();

                    // Update Local and API
                    _characterRepository.UpdateCharacter(character);
                    await _characterRepository.Save();
                    await _characterService.UpdateCharacter(character);

                    return RedirectToAction(nameof(Details), new { id });
                }
            }

            return View();
        }

        private void HandleBrokenCircuit()
        {
            TempData["CharInoperativeMsg"] = "Character Service Currently Unavailable.";
        }
    }
}
