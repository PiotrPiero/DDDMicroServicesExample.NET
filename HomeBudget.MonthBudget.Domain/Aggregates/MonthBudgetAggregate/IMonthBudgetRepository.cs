using System;
using System.Threading.Tasks;

namespace HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate
{
    public interface IMonthBudgetRepository
    {
        Task<MonthBudget> GetById(int id);
        Task<BudgetCategory> GetBudgetCategoryById(int id);
        void AddOrUpdate(MonthBudget monthBudget);
        void Remove(int id);
    }
}
