using HomeBudget.Core.Impl;
using HomeBudget.Core.Models;
using System;
using System.Collections.Generic;

namespace HomeBudget.Account.Domain.Aggregates.AccountAggregate
{
    public class Account : AggregateRoot
    {
        
        public Account() : base() { }

        public Account(DateTime created, DateTime modified, int authorId, AccountType type, Currency currency, string name, decimal state) : base(created, modified, authorId)
        {
            Type = type;
            Currency = currency;
            Name = name;
            State = state;
        }

        public AccountType Type { get; private set; }
        public Currency Currency { get; private set; }
        public string Name { get; private set; }
        public decimal State { get; private set; }

        public static Account New(int authorId, AccountType type, Currency currency, string name, decimal initialState)
        {
            var created = DateTime.Now;

            return new Account(created, created, authorId, type, currency, name, initialState);
        }

        public void ChangeState(decimal gross)
        {
            State += gross;
        }
    }
}
