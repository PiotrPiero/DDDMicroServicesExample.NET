using System;

namespace HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate
{
    public interface IMonthBudgetRepository
    {
        void AddOrUpdate(FinOperation operation);
        void Remove(Guid id);
    }
}
