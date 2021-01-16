using System;
using System.Collections.Generic;

namespace HomeBudget.Integration
{
    public class EventBusSubscriptionManager: IEventBusSubscriptionManager
    {
        private readonly List<Subscription> _subscriptions;

        public EventBusSubscriptionManager(List<Subscription> subscriptions)
        {
            _subscriptions = subscriptions;
        }

        public event EventHandler<IIntegrationEvent> OnRemoved;
        public event EventHandler<IIntegrationEvent> OnAdded;
        
        public void AddSubscription<T, Y>() where T : IIntegrationEvent where Y : IIntegrationEventHandler<T>
        {
            //_subscriptions.Add(typeof(T), typeof(Y));
        }

        public void RemoveSubscription<T, Y>() where T : IIntegrationEvent where Y : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IIntegrationEventHandler<T>> GetSubscriptionHandlers<T>(T @event) where T : IIntegrationEvent
        {
            throw new NotImplementedException();
        }
    }
}