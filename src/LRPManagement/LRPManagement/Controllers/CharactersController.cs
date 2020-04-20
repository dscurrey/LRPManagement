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
using LRPManagement.Data.Players;
using LRPManagement.Data.Skills;
using LRPManagement.Models;
using Polly.CircuitBreaker;

namespace LRPManagement.Controllers
{
    public class CharactersController : Controller
    {
        private readonly ICharacterService _characterService;
        private readonly ICharacterRepository _characterRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ISkillRepository _skillRepository;

        public CharactersController(ICharacterService characterService, ICharacterRepository characterRepository, IPlayerRepository playerRepository, ISkillRepository skillRepository)
        {
            _characterService = characterService;
            _characterRepository = characterRepository;
            _playerRepository = playerRepository;
            _skillRepository = skillRepository;
        }

        // GET: Characters
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
                            PlayerId = character.PlayerId
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
                //var character = await _characterService.GetCharacter(id.Value);
                var character = await _characterRepository.GetCharacter(id.Value);
                if (character == null)
                {
                    return NotFound();
                }

                return View(character);
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }
            return View();
        }

        // GET: Characters/Create
        public async Task<IActionResult> Create()
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
            else
            {
                ViewBag.Skills = "";
            }

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

            // TODO - Adding Skills

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

        private async Task<bool> CharacterDTOExists(int id)
        {
            var character = await _characterService.GetCharacter(id);
            return character != null;
        }

        private void HandleBrokenCircuit()
        {
            TempData["CharInoperativeMsg"] = "Character Service Currently Unavailable.";
        }
    }
}
