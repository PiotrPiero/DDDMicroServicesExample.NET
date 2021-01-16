using System;

namespace HomeBudget.Integration
{
    public class Subscription
    {
        private readonly Type _event;
        private readonly Type _handlerType;

        public Subscription(Type @event, Type handlerType)
        {
            _event = @event;
            _handlerType = handlerType;
        }

        public Type Type => _handlerType;
        public Type IntegrationEvent => _event;
    }
}