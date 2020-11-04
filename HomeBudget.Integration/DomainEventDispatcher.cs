using System.Linq;
using System.Threading.Tasks;
using HomeBudget.Core.Impl;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeBudget.Integration
{
    public static class DomainEventDispatcher
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, DbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<HomeBudget.Core.Impl.AggregateRoot>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(e => e.Entity.DomainEvents.Clear());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}