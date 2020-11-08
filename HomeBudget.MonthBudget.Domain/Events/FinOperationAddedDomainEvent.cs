using HomeBudget.Core;
using HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate;

namespace HomeBudget.MonthBudget.Domain.Events
{
    public class FinOperationAddedDomainEvent: IDomainEvent
    {
        public FinOperationAddedDomainEvent(FinOperation finOperation, string accountName)
        {
            FinOperation = finOperation;
            AccountName = accountName;
        }
        public FinOperation FinOperation { get; private set; }
        public string AccountName { get;  }
    }
}