using System.Threading;
using System.Threading.Tasks;

namespace LRPManagement.Services
{
    /// <summary>
    /// Interface for scoped processing service for API
    /// </summary>
    public interface IApiScopedProcessingService
    {
        Task DoWork(CancellationToken stoppingToken);
    }
}