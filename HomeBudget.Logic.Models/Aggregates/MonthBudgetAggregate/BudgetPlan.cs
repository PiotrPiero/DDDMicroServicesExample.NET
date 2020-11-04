using HomeBudget.Core;
using System;
using HomeBudget.Core.Impl;

namespace HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate
{
    public class BudgetPlan : IValueObject
    {
        public BudgetPlan()
        {

        }

        public BudgetPlan(int id, decimal limit, BudgetCategory budgetCategory, MonthBudget monthBudget)
        {
            if (budgetCategory == null)
                throw new ArgumentNullException(); //toDo errors

            Id = id;
            Limit = limit;
            BudgetCategory = budgetCategory;
            MonthBudget = monthBudget;
        }

        public int Id { get; private set; }
        public decimal Limit { get; private set; }
        public virtual BudgetCategory BudgetCategory { get; private set; }
        public virtual MonthBudget MonthBudget { get; private set; }

    }
}
