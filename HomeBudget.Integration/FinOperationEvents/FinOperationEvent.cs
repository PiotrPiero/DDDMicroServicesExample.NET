using HomeBudget.Core;
using System;

namespace HomeBudget.Integration.FinOperationEvents
{
    public abstract class FinOperationEvent : IIntegrationEvent
    {

        public FinOperationEvent(int finOperationId, int monthBudgetId, string accountName)
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
