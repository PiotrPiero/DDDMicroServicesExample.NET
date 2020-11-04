using System;
using System.Threading.Tasks;

namespace HomeBudget.Integration
{
    public interface IIntegrationService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IIntegrationEvent evt);
    }
}