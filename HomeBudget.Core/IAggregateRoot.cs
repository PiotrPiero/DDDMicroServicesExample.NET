using System.Collections.Generic;

namespace HomeBudget.Core
{
    public interface IAggregateRoot : IEntity
    {
        ICollection<IDomainEvent> DomainEvents { get; }
    }
}
