using HomeBudget.Core.Impl;
using System;

namespace HomeBudget.Account.Domain.Aggregates.TransferAggregate
{
    public class Transfer : AggregateRoot
    {
        public Transfer() : base()
        {

        }

        public Transfer(AccountId receiver, AccountId sender, TransferValue value, int authorId)
        {
            Receiver = receiver;
            Sender = sender;
            Value = value;
            AuthorId = authorId;
        }

        public virtual AccountId Receiver { get; private set; }
        public virtual AccountId Sender { get; private set; }
        public virtual TransferValue Value { get; private set; }


    }
}
