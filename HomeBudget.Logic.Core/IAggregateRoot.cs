using System.Collections.Generic;

namespace HomeBudget.Core.Impl
{
    public interface IAggregateRoot : IEntity
    {
        ICollection<IDomainEvent> DomainEvents { get; }
    }
}
