using HomeBudget.Core;

namespace HomeBudget.Integration.FinOperationEvents
{
    public class FinOperationAddedIntegrationEvent: FinOperationEvent
    {
        public FinOperationAddedIntegrationEvent(int finOperationId, int monthBudgetId, string accountName): base(finOperationId, monthBudgetId, accountName)
        {
        }
    }
}