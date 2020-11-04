using HomeBudget.Core;
using System;
using System.Collections.Generic;
using HomeBudget.Core.Impl;

namespace HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate
{
    public class BudgetCategory : IValueObject
    {
        public BudgetCategory() =>
            SavedOperations = new HashSet<FinOperation>();

        public int Id { get; set; }
        public string Name { get; set; }
        public bool CanHaveExpenses { get; set; }
        public bool CanHaveIncomes { get; set; }

        public virtual ICollection<FinOperation> SavedOperations { get; private set; } //readonly collection
    }
}
