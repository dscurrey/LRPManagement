using System.Threading;
using System.Threading.Tasks;
using LRPManagement.Data.Players;
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

        public ApiUpdaterService(ILogger<ApiUpdaterService> logger, IPlayerService playerService, IPlayerRepository playerRepository)
        {
            _logger = logger;

            _playerService = playerService;
            _playerRepository = playerRepository;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;

                _logger.LogInformation(
                    "API Updater Service is working. Count: {Count}", executionCount);

                await GetPlayers();



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
    }
}
