using System.Threading;
using System.Threading.Tasks;

namespace LRPManagement.Services
{
    public interface IApiScopedProcessingService
    {
        Task DoWork(CancellationToken stoppingToken);
    }
}