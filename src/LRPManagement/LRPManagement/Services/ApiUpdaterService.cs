﻿using System.Threading;
using System.Threading.Tasks;
using LRPManagement.Data.Characters;
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
        private int interval = 25000;

        private IPlayerRepository _playerRepository;
        private IPlayerService _playerService;
        private ICharacterRepository _characterRepository;
        private ICharacterService _characterService;
        private ISkillRepository _skillRepository;
        private ISkillService _skillService;
        private ICraftableRepository _itemRepository;
        private ICraftableService _itemService;

        public ApiUpdaterService(ILogger<ApiUpdaterService> logger, IPlayerService playerService, IPlayerRepository playerRepository,
            ICharacterRepository characterRepository, ICharacterService characterService, ISkillRepository skillRepository, ISkillService skillService,
            ICraftableService craftableService, ICraftableRepository craftableRepository)
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
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;

                _logger.LogInformation(
                    "API Updater Service is working. Count: {Count}", executionCount);

                await GetPlayers();
                await GetCharacters();
                await GetSkills();
                await GetItems();

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
                        if (await _playerRepository.GetPlayerRef(player.Id) != null)
                        {
                            continue;
                        }

                        var newPlayer = new Player
                        {
                            PlayerRef = player.Id,
                            FirstName = player.FirstName,
                            LastName = player.LastName,
                            AccountRef = player.AccountRef
                        };
                        _playerRepository.InsertPlayer(newPlayer);
                    }

                    await _playerRepository.Save();
                }
            }
            catch (BrokenCircuitException e)
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
                        if (await _skillRepository.GetSkillRef(skill.Id) != null)
                        {
                            continue;
                        }
                        var newSkill = new Skill
                        {
                            SkillRef = skill.Id,
                            Name = skill.Name,
                            XpCost = skill.XpCost
                        };
                        _skillRepository.InsertSkill(newSkill);
                    }

                    await _skillRepository.Save();
                }
            }
            catch (BrokenCircuitException e)
            {
                HandleBrokenCircuit();
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
                        if (await _itemRepository.GetCraftable(item.Id) != null)
                        {
                            continue;
                        }

                        var newItem = new Craftable
                        {
                            Name = item.Name,
                            Requirement = item.Requirement,
                            Materials = item.Materials,
                            Effect = item.Effect,
                            Form = item.Form
                        };
                        _itemRepository.InsertCraftable(newItem);
                        await _itemRepository.Save();
                    }
                }
            }
            catch (BrokenCircuitException e)
            {
                _logger.LogWarning("Broken Circuit");
            }
        }
    }
}
