using HomeBudget.Core;
using HomeBudget.Core.Impl;
using HomeBudget.Core.Models;

namespace HomeBudget.Account.Domain.Aggregates.TransferAggregate
{
    public class TransferValue : IValueObject
    {
        public TransferValue(decimal value, Currency currency)
        {
            Value = value;
            Currency = currency;
        }

        public decimal Value { get; private set; }
        public Currency Currency { get; private set; }
    }
}
