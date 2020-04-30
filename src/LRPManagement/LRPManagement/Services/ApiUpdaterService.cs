using System;
using System.Threading;
using System.Threading.Tasks;
using LRPManagement.Data.Bonds;
using LRPManagement.Data.Characters;
using LRPManagement.Data.CharacterSkills;
using LRPManagement.Data.Craftables;
using LRPManagement.Data.Players;
using LRPManagement.Data.Skills;
using LRPManagement.Models;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;

namespace LRPManagement.Services
{
    public class ApiUpdaterService : IApiScopedProcessingService
    {
        private int executionCount = 0;
        private readonly ILogger<ApiUpdaterService> _logger;
        private int interval = 20000; // Secs * 1000

        private IPlayerRepository _playerRepository;
        private IPlayerService _playerService;
        private ICharacterRepository _characterRepository;
        private ICharacterService _characterService;
        private ISkillRepository _skillRepository;
        private ISkillService _skillService;
        private ICraftableRepository _itemRepository;
        private ICraftableService _itemService;
        private IBondRepository _bondRepository;
        private IBondService _bondService;
        private ICharacterSkillService _charSkillService;
        private ICharacterSkillRepository _charSkillRepository;

        public ApiUpdaterService(ILogger<ApiUpdaterService> logger, IPlayerService playerService, IPlayerRepository playerRepository,
            ICharacterRepository characterRepository, ICharacterService characterService, ISkillRepository skillRepository, ISkillService skillService,
            ICraftableService craftableService, ICraftableRepository craftableRepository,
            IBondService bondService, IBondRepository bondRepository, ICharacterSkillService characterSkillService, ICharacterSkillRepository characterSkillRepository)
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

                _logger.LogInformation(
                    "API Updater Service is working. Count: {Count}", executionCount);

                try
                {
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
                var players = await _playerService.GetAll();
                if (players != null)
                {
                    foreach (var player in players)
                    {
                        var existPlayer = await _playerRepository.GetPlayerRef(player.Id);
                        if (existPlayer != null)
                        {
                            if (existPlayer.FirstName.Equals
                                    (player.FirstName, StringComparison.CurrentCultureIgnoreCase) &&
                                existPlayer.LastName.Equals(player.LastName, StringComparison.CurrentCultureIgnoreCase))
                            {
                                continue;
                            }
                            else
                            {
                                var updPlayer = new Player
                                {
                                    AccountRef = player.AccountRef,
                                    FirstName = player.FirstName,
                                    //Id = existPlayer.Id,
                                    LastName = player.LastName,
                                    PlayerRef = player.Id
                                };
                                _playerRepository.UpdatePlayer(updPlayer);
                            }
                        }
                        else
                        {
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
                var characters = await _characterService.GetAll();
                if (characters != null)
                {
                    foreach (var character in characters)
                    {
                        if (await _playerRepository.GetPlayer(character.PlayerId) != null)
                        {
                            var existChar = await _characterRepository.GetCharacterRef(character.Id);
                            if (existChar != null)
                            {
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
                var skills = await _skillService.GetAll();
                if (skills != null)
                {
                    foreach (var skill in skills)
                    {
                        var existSkill = await _skillRepository.GetSkillRef(skill.Id);
                        if (existSkill != null)
                        {
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
                var items = await _itemService.GetAll();
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        var existItem = await _itemRepository.GetCraftableRef(item.Id);
                        if (existItem != null)
                        {
                            if (item.Name.Equals(existItem.Name) && item.Requirement.Equals(existItem.Requirement) && item.Materials.Equals(existItem.Materials) && item.Effect.Equals(existItem.Effect) && item.Form.Equals(existItem.Form))
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
                var bonds = await _bondService.Get();
                if (bonds != null)
                {
                    foreach (var bond in bonds)
                    {
                        // Ensure that bond is not already present and necessary items are stored
                        if (await BondExists(bond.ItemId, bond.CharacterId) || await _characterRepository.GetCharacterRef(bond.CharacterId) == null || await _itemRepository.GetCraftable(bond.ItemId) == null)
                        {
                            continue;
                        }

                        _bondRepository.Insert(bond);
                        await _bondRepository.Save();
                    }
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
                {
                    foreach (var charSkill in charSkills)
                    {
                        // Ensure that charSKill is not already present and necessary items are stored
                        if (await CharSkillExists(charSkill.SkillId, charSkill.CharacterId) ||
                            await _characterRepository.GetCharacterRef(charSkill.CharacterId) == null ||
                            await _skillRepository.GetSkill(charSkill.SkillId) == null)
                        {
                            continue;
                        }

                        _charSkillRepository.AddSkillToCharacter(charSkill.SkillId, charSkill.CharacterId);
                        await _charSkillRepository.Save();
                    }
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
