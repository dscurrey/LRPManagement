using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LRPManagement.Services
{
    public class ApiUpdateHostedService : BackgroundService
    {
        private readonly ILogger<ApiUpdateHostedService> _logger;

        public IServiceProvider Services { get; }

        public ApiUpdateHostedService(IServiceProvider services,
            ILogger<ApiUpdateHostedService> logger)
        {
            Services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("API Update Hosted Service is running.");

            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation("API Update Hosted Service is working.");

            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService = scope.ServiceProvider
                    .GetRequiredService<IApiScopedProcessingService>();

                await scopedProcessingService.DoWork(cancellationToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("API Update Hosted Service is stopping.");

            await Task.CompletedTask;
        }
    }
}
