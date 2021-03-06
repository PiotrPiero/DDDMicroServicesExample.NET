﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HomeBudget.Integration.Logging
{
    public class IntegrationEventService: IIntegrationEventService
    {
        private readonly IntegrationEventLogContext _ctx;
        private List<Type> _eventTypes;
        
        public IntegrationEventService(IntegrationEventLogContext ctx)
        {
            _ctx = ctx;

            _eventTypes = Assembly.GetAssembly(this.GetType())
                .GetTypes()
                .Where(x => x.Name.EndsWith("IntegrationEvent"))
                .ToList();
        }
        
        public async Task<IEnumerable<IntegrationEventLog>> GetAll(Guid transactionId, EventStatus status)
        {
            var res = await _ctx.IntegrationEventLogs
                .Where(x => x.Status == status && x.TransactionId == transactionId)
                .OrderBy(x => x.Created)
                .ToListAsync();

            return res.Select(x => x.LoadEventValue(_eventTypes.Find(e => e.FullName == x.Name)));

        }

        public Task SaveEventAsync(IIntegrationEvent @event, IDbContextTransaction transactionContext)
        {
            if(@event == null || transactionContext == null)
                throw new ArgumentNullException($"{nameof(@event)},{nameof(transactionContext)}");
            
            var newLog = new IntegrationEventLog(transactionContext.TransactionId, @event);
            
            _ctx.IntegrationEventLogs.Add(newLog);

            return _ctx.SaveChangesAsync();
        }

        public Task ChangeEventStatus(Guid id, EventStatus newStatus)
        {
            var log = _ctx.IntegrationEventLogs.Single(x => x.EventId == id);

            log.Status = newStatus;
            
            _ctx.IntegrationEventLogs.Update(log);
            
            return _ctx.SaveChangesAsync();
        }
    }
}