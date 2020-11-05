using HomeBudget.Core;

namespace HomeBudget.MonthBudget.Domain.Events
{
    public class FinOperationAddedDomainEvent: IDomainEvent
    {
        public FinOperationAddedDomainEvent(int finOperationId, int monthBudgetId, string accountName)
        {
            FinOperationId = finOperationId;
            MonthBudgetId = monthBudgetId;
            AccountName = accountName;
        }
        public int FinOperationId { get; private set; }
        public int MonthBudgetId { get; private set; }
        public string AccountName { get;  }
    }
}