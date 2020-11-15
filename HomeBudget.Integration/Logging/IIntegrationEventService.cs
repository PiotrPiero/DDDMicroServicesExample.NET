using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace HomeBudget.Integration.Logging
{
    public interface IIntegrationEventService
    {
        Task<IEnumerable<IntegrationEventLog>> GetAll(Guid transactionId,  EventStatus status);
        Task SaveEventAsync(IIntegrationEvent @event, IDbContextTransaction  transactionContext);
        Task ChangeEventStatus(Guid id, EventStatus newStatus);
    }
}