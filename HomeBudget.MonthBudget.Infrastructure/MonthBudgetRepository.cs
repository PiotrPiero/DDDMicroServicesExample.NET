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

        public void AddOrUpdate(FinOperation operation)
        {
            if (operation.Id.HasValue)
            {
                try
                {
                    _ctx.FinOperations.Update(operation);
                    _ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Cannot update fin operation for account {operation.AccountName}, operatonId: {operation.Id}");
                    throw ex;
                }
            }
            else
            {
                try
                {
                    _ctx.FinOperations.Add(operation); 
                    _ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Cannot save fin operation for account {operation.AccountName}");
                    throw ex;
                }
            }
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