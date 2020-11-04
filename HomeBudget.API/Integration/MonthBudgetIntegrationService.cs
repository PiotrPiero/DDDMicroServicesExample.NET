using System;
using System.Threading.Tasks;
using HomeBudget.Integration;
using HomeBudget.Integration.Logging;
using HomeBudget.MonthBudget.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeBudget.MonthBudget.API.Integration
{
    public class MonthBudgetIntegrationService : IIntegrationService
    {
        private readonly IIntegrationEventLogger _integrationLogger;
        private readonly MonthBudgetContext _monthBudgetContext;
        private readonly IEventBus _eventBus;
        private readonly ILogger _logger;
        
        public MonthBudgetIntegrationService(IEventBus eventBus,
            ILogger logger,
            IIntegrationEventLogger integrationLogger,
            MonthBudgetContext monthBudgetContext
            )
        {
            _integrationLogger = integrationLogger;
            _monthBudgetContext = monthBudgetContext;
            _logger = logger;
            _eventBus = eventBus;
        }

        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            var newEvents = await _integrationLogger.GetAll(transactionId, EventStatus.New);

            foreach (var newEvent in newEvents)
            {
                try
                {
                    _logger.LogInformation($"Publish integration event with Id: {newEvent.EventId}");
                    await _integrationLogger.ChangeEventStatus(newEvent.EventId, EventStatus.Pending);
                    _eventBus.Publish(newEvent.Event);
                    await _integrationLogger.ChangeEventStatus(newEvent.EventId, EventStatus.Published);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error publishing integration event {newEvent.EventId}");
                    await _integrationLogger.ChangeEventStatus(newEvent.EventId, EventStatus.Failed);
                }
            }
        }

        public async Task AddAndSaveEventAsync(IIntegrationEvent integrationEvent)
        {
            _logger.LogInformation($"Save new integration event {integrationEvent.Id}");

            await _integrationLogger.SaveEventAsync(integrationEvent, _monthBudgetContext.Database.CurrentTransaction);
        }
    }
}