using System;

namespace HomeBudget.Integration
{
    public class IIntegrationEvent
    {
        public Guid Id => Guid.NewGuid();
        public DateTime Created => DateTime.Now;
    }
}