using System;

namespace HomeBudget.Account.Domain.Aggregates.AccountAggregate
{
    public class TransferId
    {
        public TransferId(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }
}