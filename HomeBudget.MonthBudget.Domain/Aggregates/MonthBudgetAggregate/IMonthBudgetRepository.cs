using System;
using System.Threading.Tasks;

namespace HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate
{
    public interface IMonthBudgetRepository
    {
        void AddOrUpdate(FinOperation operation);
        void Remove(int id);
    }
}
