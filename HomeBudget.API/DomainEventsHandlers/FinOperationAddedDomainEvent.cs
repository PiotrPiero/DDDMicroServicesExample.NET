using System.Threading;
using System.Threading.Tasks;
using HomeBudget.Integration;
using HomeBudget.Integration.FinOperationEvents;
using HomeBudget.MonthBudget.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HomeBudget.MonthBudget.API.DomainEventsHandlers
{
    public class FinOperationAddedDomainEventHandler: INotificationHandler<FinOperationAddedDomainEvent>
    {
        private readonly ILogger _logger;
        private readonly IIntegrationService _integrationService;

        public FinOperationAddedDomainEventHandler(ILogger logger, IIntegrationService integrationService)
        {
            _logger = logger;
            _integrationService = integrationService;
        }

        public async Task Handle(FinOperationAddedDomainEvent notification, CancellationToken cancellationToken)
        {
           _logger.LogInformation($"FinOperationAdded, ID={notification.FinOperationId}");

           await _integrationService.AddAndSaveEventAsync(new FinOperationAddedIntegrationEvent(notification.FinOperationId,
               notification.MonthBudgetId, notification.AccountName));
        }
    }
}