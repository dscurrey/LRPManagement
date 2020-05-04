using DTO;
using LRPManagement.Data.Characters;
using LRPManagement.Data.CharacterSkills;
using LRPManagement.Data.Players;
using LRPManagement.Data.Skills;
using LRPManagement.Models;
using LRPManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Polly.CircuitBreaker;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Controllers
{
    /// <summary>
    /// Character controller used for interactions involving Characters.
    /// Requires login
    /// </summary>
    [Authorize]
    public class CharactersController : Controller
    {
        private readonly ICharacterService _characterService;
        private readonly ICharacterRepository _characterRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly ICharacterSkillRepository _charSkillRepository;

        /// <summary>
        /// Creates an instance of CharactersController
        /// </summary>
        /// <param name="characterService">A class to allow communication with the Characters API</param>
        /// <param name="characterRepository">Repository containing characters</param>
        /// <param name="playerRepository">Repository to store players</param>
        /// <param name="skillRepository">Repository to store skills</param>
        /// <param name="charSkillRepository">Repository to store char skill relationship</param>
        public CharactersController(ICharacterService characterService,
            ICharacterRepository characterRepository,
            IPlayerRepository playerRepository,
            ISkillRepository skillRepository,
            ICharacterSkillRepository charSkillRepository)
        {
            _characterService = characterService;
            _characterRepository = characterRepository;
            _playerRepository = playerRepository;
            _skillRepository = skillRepository;
            _charSkillRepository = charSkillRepository;
        }

        // GET: Characters
        /// <summary>
        /// Displays a list of all characters
        /// </summary>
        /// <returns>View containing a list of all characters</returns>
        [Authorize(Policy = "StaffOnly")]
        public async Task<IActionResult> Index()
        {
            TempData["CharInoperativeMsg"] = "";

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
        /// <summary>
        /// Displays a character's details
        /// </summary>
        /// <param name="id">Id of the specified character</param>
        /// <returns>View containing character details</returns>
        public async Task<IActionResult> Details(int? id)
        {
            TempData["CharInoperativeMsg"] = "";
            if (id == null) return NotFound();

            try
            {
                var character = await _characterRepository.GetCharacter(id.Value);
                if (character == null) return NotFound();

                var player = await _playerRepository.GetPlayerAccountRef(User.Identity.Name);
                if (User.IsInRole("Admin") || User.IsInRole("Referee") || character.PlayerId == player.Id)
                {
                    var skills = new List<Skill>();
                    foreach (var charSkill in character.CharacterSkills) skills.Add(charSkill.Skill);

                    var charView = new CharacterDetailsViewModel
                    {
                        Id = character.Id,
                        IsActive = character.IsActive,
                        Name = character.Name,
                        PlayerId = character.PlayerId,
                        Skills = skills,
                        Xp = character.Xp,
                        Items = character.Bond.ToList()
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
        /// <summary>
        /// Create populates information in the create form
        /// </summary>
        /// <returns>View containing data required to populate form</returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Takes the POST from create form
        /// </summary>
        /// <param name="characterDto">The character to be created</param>
        /// <returns>Redirects to index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlayerId,Name,IsActive,IsRetired")]
            CharacterDTO characterDto)
        {
            TempData["CharInoperativeMsg"] = "";

            try
            {
                var player = await _playerRepository.GetPlayerAccountRef(User.Identity.Name);
                if (player == null) return View();

                characterDto.PlayerId = player.Id;
                characterDto.Xp = 8;
                var resp = await _characterService.CreateCharacter(characterDto);
                if (resp == null)
                    // Unsuccessful/Error
                    return View(characterDto);
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Characters/Edit/5
        /// <summary>
        /// Populates information in the edit form
        /// </summary>
        /// <returns>View containing data required to populate form</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var character = await _characterService.GetCharacter(id.Value);
                if (character != null) return View(character);
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
        /// <summary>
        /// Edits a specified character in the database
        /// </summary>
        /// <param name="id">Id of the character to be edited</param>
        /// <param name="characterDTO">The edited character</param>
        /// <returns>View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,PlayerId,Name,IsActive,IsRetired")]
            CharacterDTO characterDTO)
        {
            if (id != characterDTO.Id) return NotFound();

            try
            {
                var resp = await _characterService.UpdateCharacter(characterDTO);
                if (resp != null) return RedirectToAction(nameof(Index));

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
        /// <summary>
        /// Populates the delete view
        /// </summary>
        /// <param name="id">Id for the character to be deleted</param>
        /// <returns>Delete Confirmation containing delete target</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var character = await _characterService.GetCharacter(id.Value);
                if (character != null) return View(character);
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return NotFound();
        }

        // POST: Characters/Delete/5
        /// <summary>
        /// Deletes a specified character
        /// </summary>
        /// <param name="id">The id for the chosen character</param>
        /// <returns>View</returns>
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TempData["CharInoperativeMsg"] = "";
            try
            {
                await _characterService.DeleteCharacter(id);

                await _characterRepository.DeleteCharacter(id);
                await _characterRepository.Save();
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> AddSkill(int? id)
        {
            if (id == null) return NotFound();

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

        /// <summary>
        /// Creates a characterskill
        /// </summary>
        /// <param name="id"></param>
        /// <param name="charSkill">The character skill to be created</param>
        /// <returns>View</returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSkill(int id, [Bind("CharId, SkillId")] CharacterSkillViewModel charSkill)
        {
            var skill = await _skillRepository.GetSkill(charSkill.SkillId);
            var character = await _characterRepository.GetCharacter(charSkill.CharId);

            if (skill != null && character != null)
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

                    return RedirectToAction(nameof(Details), new {id});
                }

            return View();
        }

        /// <summary>
        /// Sets a specified character's IsRetired to TRUE
        /// </summary>
        /// <param name="id">Id of specified character</param>
        /// <returns></returns>
        public async Task<IActionResult> SetRetire(int id)
        {
            var character = await _characterRepository.GetCharacter(id);
            if (character == null) return NotFound();

            if (!character.IsRetired)
            {
                character.IsActive = false;
                character.IsRetired = true;
                _characterRepository.UpdateCharacter(character);
                await _characterRepository.Save();
                await _characterService.UpdateCharacter(character);
            }

            return RedirectToAction(nameof(Details), "Players", new {id = character.PlayerId});
        }

        /// <summary>
        /// Sets a specified character as active
        /// </summary>
        /// <param name="id">ID for the specified character</param>
        /// <returns>Redirects to details</returns>
        public async Task<IActionResult> SetActive(int id)
        {
            var character = await _characterRepository.GetCharacter(id);
            if (character == null) return NotFound();

            if (!character.IsRetired && !character.IsActive)
            {
                character.IsActive = true;
                _characterRepository.UpdateCharacter(character);
                await _characterRepository.Save();
                await _characterService.UpdateCharacter(character);
            }

            return RedirectToAction(nameof(Details), "Players", new {id = character.PlayerId});
        }

        private void HandleBrokenCircuit()
        {
            TempData["CharInoperativeMsg"] = "Character Service Currently Unavailable.";
            ViewBag.CharactersError = "Character Service is Currently Unavailable";
        }
    }
}