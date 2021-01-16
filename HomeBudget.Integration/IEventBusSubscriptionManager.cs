using System;
using System.Collections.Generic;

namespace HomeBudget.Integration
{
    public interface IEventBusSubscriptionManager
    {
        event EventHandler<IIntegrationEvent> OnRemoved;
        event EventHandler<IIntegrationEvent> OnAdded;

        void AddSubscription<T,Y>() where T : IIntegrationEvent where Y : IIntegrationEventHandler<T>;
        void RemoveSubscription<T,Y>() where T : IIntegrationEvent where Y : IIntegrationEventHandler<T>;

        IEnumerable<IIntegrationEventHandler<T>> GetSubscriptionHandlers<T>(T @event) where T: IIntegrationEvent;
    }
}