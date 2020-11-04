using System;

namespace HomeBudget.Account.Domain.Aggregates.TransferAggregate
{
    public class AccountId
    {
        public AccountId(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
}
