﻿namespace HomeBudget.Integration
{
    public interface IEventBus
    {
        void Publish(IIntegrationEvent @event);
        
        void Subscribe<T, TH>()
            where T : IIntegrationEvent
            where TH : IIntegrationEventHandler<T>;
        
        // void SubscribeDynamic<TH>(string eventName)
        //     where TH : IDynamicIntegrationEventHandler;
        //
        // void UnsubscribeDynamic<TH>(string eventName)
        //     where TH : IDynamicIntegrationEventHandler;
        
        void Unsubscribe<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IIntegrationEvent;
    }
}