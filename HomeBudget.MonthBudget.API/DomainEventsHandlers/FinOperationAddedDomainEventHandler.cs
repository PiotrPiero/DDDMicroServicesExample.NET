using System;
using System.Threading;
using System.Threading.Tasks;
using HomeBudget.Integration;
using HomeBudget.Integration.FinOperationEvents;
using HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate;
using HomeBudget.MonthBudget.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HomeBudget.MonthBudget.API.DomainEventsHandlers
{
    public class FinOperationAddedDomainEventHandler: INotificationHandler<FinOperationAddedDomainEvent>
    {
        private readonly ILogger _logger;
        private readonly IIntegrationService _integrationService;
        private readonly IMonthBudgetRepository _monthBudgetRepository;
        
        public FinOperationAddedDomainEventHandler(ILogger<FinOperationAddedDomainEventHandler> logger, IIntegrationService integrationService, IMonthBudgetRepository monthBudgetRepository)
        {
            _logger = logger;
            _integrationService = integrationService;
            _monthBudgetRepository = monthBudgetRepository;
        }

        public async Task Handle(FinOperationAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"FinOperationAdded, ID={notification.FinOperation.Id}");

                
                await _integrationService.AddAndSaveEventAsync(new FinOperationAddedIntegrationEvent(
                    notification.FinOperation.Id.Value,
                    notification.FinOperation.MonthBudget.Id.Value,
                    notification.AccountName));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot save fin operation for account: ${notification.AccountName}");
            }
        }
    }
}