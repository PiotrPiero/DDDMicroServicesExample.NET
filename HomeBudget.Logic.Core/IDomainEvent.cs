using System;
using MediatR;

namespace HomeBudget.Core
{
    public interface IDomainEvent: INotification
    {
        public DateTime Created => DateTime.Now;
    }
}
