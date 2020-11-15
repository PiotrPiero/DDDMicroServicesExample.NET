using System;
using Newtonsoft.Json;

namespace HomeBudget.Integration.Logging
{
    public class IntegrationEventLog
    {
        public IntegrationEventLog()
        {
            
        }

        public IntegrationEventLog(Guid transactionId, IIntegrationEvent @event)
        {
            EventId = @event.Id;
            Name = @event.GetType().FullName;
            TransactionId = transactionId;
            Created = @event.Created;
            Status = EventStatus.New;
            Event = @event;
            JsonValue = JsonConvert.SerializeObject(@event);
        }
        public int Id { get; private set; }
        public Guid EventId { get; private set; }
        public string Name { get; private set; }
        public Guid TransactionId { get; private set; }
        public DateTime Created { get; private set; }
        public EventStatus Status { get; set; }
        public IIntegrationEvent Event { get; private set; }
        public string JsonValue { get; private set; }

        public IntegrationEventLog LoadEventValue(Type eventType)
        {
            Event = JsonConvert.DeserializeObject(JsonValue, eventType) as IIntegrationEvent;
            return this;
        }
    }
}