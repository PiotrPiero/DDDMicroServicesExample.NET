using HomeBudget.Core.Impl;
using System;

namespace HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate
{
    public class FinOperation : Entity
    {
        public FinOperation() : base() { }

        protected FinOperation(
            DateTime created,
            DateTime modified,
            DateTime operationDate,
            string name,
            string accountName,
            FinOperationType type,
            BudgetCategory budgetCategory,
            int authorId,
            FinOperationValue value,
            MonthBudget monthBudget
            ) : base(created, modified, authorId)
        {
            Name = name;
            AccountName = accountName;
            Type = type;
            BudgetCategory = budgetCategory;
            OperationDate = operationDate;
            MonthBudget = monthBudget;
            OperationValue = value;
        }

        public virtual FinOperationValue OperationValue { get; private set; }
        public string AccountName { get; private set; }
        public DateTime OperationDate { get; private set; }
        public FinOperationType Type { get; private set; }
        public string Name { get; private set; }
        public string Description { get; set; }
        public virtual BudgetCategory BudgetCategory { get; private set; }
        public virtual MonthBudget MonthBudget { get; private set; }

        public static FinOperation New(string name,string accountName, FinOperationType type, DateTime operationDate, BudgetCategory budgetCategory, MonthBudget monthBudget, int authorId, FinOperationValue value) =>
             new FinOperation(DateTime.Now, DateTime.Now, operationDate, name, accountName, type, budgetCategory, authorId, value, monthBudget);

    }
}
