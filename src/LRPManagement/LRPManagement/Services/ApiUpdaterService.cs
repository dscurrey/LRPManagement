using LRPManagement.Data.Bonds;
using LRPManagement.Data.Characters;
using LRPManagement.Data.CharacterSkills;
using LRPManagement.Data.Craftables;
using LRPManagement.Data.Players;
using LRPManagement.Data.Skills;
using LRPManagement.Models;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LRPManagement.Services
{
    /// <summary>
    /// Service which runs in the background and pulls data from microservice APIs, ensuring synchronicity with DB.
    /// </summary>
    public class ApiUpdaterService : IApiScopedProcessingService
    {
        private int executionCount = 0;
        private readonly ILogger<ApiUpdaterService> _logger;

        private readonly int interval = 15000; // Secs * 1000

        private readonly IPlayerRepository _playerRepository;
        private readonly IPlayerService _playerService;
        private readonly ICharacterRepository _characterRepository;
        private readonly ICharacterService _characterService;
        private readonly ISkillRepository _skillRepository;
        private readonly ISkillService _skillService;
        private readonly ICraftableRepository _itemRepository;
        private readonly ICraftableService _itemService;
        private readonly IBondRepository _bondRepository;
        private readonly IBondService _bondService;
        private readonly ICharacterSkillService _charSkillService;
        private readonly ICharacterSkillRepository _charSkillRepository;

        public ApiUpdaterService(ILogger<ApiUpdaterService> logger,
            IPlayerService playerService,
            IPlayerRepository playerRepository,
            ICharacterRepository characterRepository,
            ICharacterService characterService,
            ISkillRepository skillRepository,
            ISkillService skillService,
            ICraftableService craftableService,
            ICraftableRepository craftableRepository,
            IBondService bondService,
            IBondRepository bondRepository,
            ICharacterSkillService characterSkillService,
            ICharacterSkillRepository characterSkillRepository)
        {
            _logger = logger;

            _playerService = playerService;
            _playerRepository = playerRepository;
            _characterService = characterService;
            _characterRepository = characterRepository;
            _skillService = skillService;
            _skillRepository = skillRepository;
            _itemService = craftableService;
            _itemRepository = craftableRepository;
            _bondRepository = bondRepository;
            _bondService = bondService;
            _charSkillService = characterSkillService;
            _charSkillRepository = characterSkillRepository;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;

                _logger.LogInformation
                (
                    "API Updater Service is working. Count: {Count}", executionCount
                );

                try
                {
                    // Call all services and populate db
                    await GetPlayers();
                    await GetCharacters();
                    await GetSkills();
                    await GetItems();
                    await GetBonds();
                    await GetCharSkills();
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, "API Service Error Occurred.");
                }

                await Task.Delay(interval, stoppingToken);
            }
        }

        private async Task GetPlayers()
        {
            try
            {
                // Gets all players in Players API, if players are obtained, carry out operations
                var players = await _playerService.GetAll();
                if (players != null)
                {
                    foreach (var player in players)
                    {
                        // For each player found, check if it already exists in the db
                        var existPlayer = await _playerRepository.GetPlayerRef(player.Id);
                        if (existPlayer != null)
                            // If exists, update if needed
                        {
                            if (existPlayer.FirstName.Equals
                                    (player.FirstName, StringComparison.CurrentCultureIgnoreCase) &&
                                existPlayer.LastName.Equals(player.LastName, StringComparison.CurrentCultureIgnoreCase))
                            {
                                continue;
                            }
                            else
                            {
                                var updPlayer = existPlayer;
                                updPlayer.FirstName = player.FirstName;
                                updPlayer.LastName = player.LastName;
                                _playerRepository.UpdatePlayer(updPlayer);
                            }
                        }
                        else
                        {
                            // Else, create new player in DB
                            var newPlayer = new Player
                            {
                                PlayerRef = player.Id,
                                FirstName = player.FirstName,
                                LastName = player.LastName,
                                AccountRef = player.AccountRef
                            };
                            _playerRepository.InsertPlayer(newPlayer);
                        }
                    }

                    await _playerRepository.Save();
                }
            }
            catch (BrokenCircuitException)
            {
                _logger.LogWarning("Broken Circuit");
            }
        }

        private async Task GetCharacters()
        {
            try
            {
                // Get characters from api
                var characters = await _characterService.GetAll();
                if (characters != null)
                    // If characters are found, check if they already exist in db
                    foreach (var character in characters)
                        if (await _playerRepository.GetPlayer(character.PlayerId) != null)
                        {
                            var existChar = await _characterRepository.GetCharacterRef(character.Id);
                            if (existChar != null)
                            {
                                // If character exists, update if required
                                if (character.IsRetired == existChar.IsRetired &&
                                    character.IsActive == existChar.IsActive && character.Name.Equals
                                        (existChar.Name) && character.Xp == existChar.Xp)
                                {
                                    continue;
                                }
                                else
                                {
                                    var updChar = new Character
                                    {
                                        CharacterRef = character.Id,
                                        IsActive = character.IsActive,
                                        Name = character.Name,
                                        IsRetired = character.IsRetired,
                                        PlayerId = character.PlayerId,
                                        Xp = character.Xp,
                                        Id = existChar.Id
                                    };
                                    _characterRepository.UpdateCharacter(updChar);
                                }
                            }
                            else
                            {
                                // Else, create new character in DB
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
            catch (BrokenCircuitException)
            {
                _logger.LogWarning("Broken Circuit");
            }
        }

        private async Task GetSkills()
        {
            try
            {
                // Get skills from api
                var skills = await _skillService.GetAll();
                if (skills != null)
                {
                    // If skills are found, check if each exists in db
                    foreach (var skill in skills)
                    {
                        var existSkill = await _skillRepository.GetSkillRef(skill.Id);
                        if (existSkill != null)
                        {
                            // If skill exists, update if required
                            if (skill.Name == existSkill.Name && skill.XpCost == existSkill.XpCost)
                            {
                                continue;
                            }
                            else
                            {
                                var updSkill = new Skill
                                {
                                    Name = skill.Name,
                                    XpCost = skill.XpCost,
                                    SkillRef = skill.Id,
                                    Id = existSkill.Id
                                };
                                _skillRepository.UpdateSkill(updSkill);
                            }
                        }
                        else
                        {
                            // If skill doesn't exist, create new 
                            var newSkill = new Skill
                            {
                                SkillRef = skill.Id,
                                Name = skill.Name,
                                XpCost = skill.XpCost,
                                Id = skill.Id
                            };
                            _skillRepository.InsertSkill(newSkill);
                        }
                    }

                    await _skillRepository.Save();
                }
            }
            catch (BrokenCircuitException)
            {
                _logger.LogWarning("Broken Circuit");
            }
        }

        private async Task GetItems()
        {
            try
            {
                // Get list of items from item API
                var items = await _itemService.GetAll();
                if (items != null)
                {
                    // Check each, to see if it already exists in DB
                    foreach (var item in items)
                    {
                        var existItem = await _itemRepository.GetCraftableRef(item.Id);
                        if (existItem != null)
                        {
                            // If it exists, update if needed, else continue loop
                            if (item.Name.Equals(existItem.Name) && item.Requirement.Equals
                                (existItem.Requirement) && item.Materials.Equals
                                (existItem.Materials) && item.Effect.Equals(existItem.Effect) && item.Form.Equals
                                (existItem.Form))
                            {
                                continue;
                            }
                            else
                            {
                                var updItem = new Craftable
                                {
                                    Effect = item.Effect,
                                    Form = item.Form,
                                    Id = existItem.Id,
                                    Materials = item.Materials,
                                    Name = item.Name,
                                    Requirement = item.Requirement
                                };
                                _itemRepository.UpdateCraftable(updItem);
                            }
                        }
                        else
                        {
                            // Item doesn't exist, create new
                            var newItem = new Craftable
                            {
                                Name = item.Name,
                                Requirement = item.Requirement,
                                Materials = item.Materials,
                                Effect = item.Effect,
                                Form = item.Form,
                                ItemRef = item.Id,
                                Id = item.Id
                            };
                            _itemRepository.InsertCraftable(newItem);
                        }
                    }

                    await _itemRepository.Save();
                }
            }
            catch (BrokenCircuitException)
            {
                _logger.LogWarning("Broken Circuit");
            }
        }

        private async Task GetBonds()
        {
            try
            {
                // Get list of bonds from Items API
                var bonds = await _bondService.Get();
                if (bonds != null)
                    // Check each bond against the database
                    foreach (var bond in bonds)
                    {
                        // Ensure that bond is not already present and necessary items are stored
                        if (await BondExists(bond.ItemId, bond.CharacterId) ||
                            await _characterRepository.GetCharacter(bond.CharacterId) == null ||
                            await _itemRepository.GetCraftable(bond.ItemId) == null)
                        {
                            continue;
                        }

                        _bondRepository.Insert(bond);
                        await _bondRepository.Save();
                    }
            }
            catch (BrokenCircuitException)
            {
                _logger.LogWarning("Broken Circuit");
            }
        }

        private async Task GetCharSkills()
        {
            try
            {
                var charSkills = await _charSkillService.Get();
                if (charSkills != null)
                    foreach (var charSkill in charSkills)
                    {
                        // Ensure that charSKill is not already present and necessary items are stored
                        if (await CharSkillExists(charSkill.SkillId, charSkill.CharacterId) ||
                            await _characterRepository.GetCharacter(charSkill.CharacterId) == null ||
                            await _skillRepository.GetSkill(charSkill.SkillId) == null)
                        {
                            continue;
                        }

                        _charSkillRepository.AddSkillToCharacter(charSkill.SkillId, charSkill.CharacterId);
                        await _charSkillRepository.Save();
                    }
            }
            catch (BrokenCircuitException)
            {
                _logger.LogWarning("Broken Circuit");
            }
        }

        private async Task<bool> BondExists(int itemId, int charId)
        {
            var bonds = await _bondRepository.GetMatch(charId, itemId);
            return bonds != null;
        }

        private async Task<bool> CharSkillExists(int skillId, int charId)
        {
            var charSkill = await _charSkillRepository.GetMatch(charId, skillId);
            return charSkill != null;
        }
    }
}