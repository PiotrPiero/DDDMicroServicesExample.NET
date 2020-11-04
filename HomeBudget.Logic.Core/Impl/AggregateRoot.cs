using System;
using System.Collections.Generic;

namespace HomeBudget.Core.Impl
{
    public class AggregateRoot : Entity, IAggregateRoot
    {
        public AggregateRoot()
        {
            DomainEvents = new List<IDomainEvent>();
        }

        public AggregateRoot(int id, DateTime created, DateTime modified, int authorId) :
            base(id, created, modified, authorId)
        {
        }

        public AggregateRoot(DateTime created, DateTime modified, int authorId) :
            base(created, modified, authorId)
        {
        }
        
        public ICollection<IDomainEvent> DomainEvents { get; }
    }
}
