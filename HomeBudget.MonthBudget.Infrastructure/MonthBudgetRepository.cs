using System;
using System.Linq;
using System.Threading.Tasks;
using HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeBudget.MonthBudget.Infrastructure
{
    public class MonthBudgetRepository: IMonthBudgetRepository
    {
        private readonly MonthBudgetContext _ctx;
        private readonly ILogger<MonthBudgetRepository> _logger;
        
        public MonthBudgetRepository(MonthBudgetContext ctx, ILogger<MonthBudgetRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public Task<Domain.Aggregates.MonthBudgetAggregate.MonthBudget> GetById(int id)
        {
            return _ctx.MonthBudgets
                .Include(x => x.FinOperations)
                .SingleAsync(x => x.Id == id);
        }

        public Task<BudgetCategory> GetBudgetCategoryById(int id)
        {
            return _ctx.BudgetCategories.SingleAsync(x => x.Id == id);
        }

        public void AddOrUpdate(Domain.Aggregates.MonthBudgetAggregate.MonthBudget monthBudget)
        {
            if (monthBudget.Id.HasValue)
                _ctx.MonthBudgets.Update(monthBudget);
            else
                _ctx.MonthBudgets.Add(monthBudget);
        }

        public void Remove(int id)
        {
            try
            {
                //todo usuwanie active/unactive

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot remove fin operation {id}");
                throw ex;
            }
        }
    }
}