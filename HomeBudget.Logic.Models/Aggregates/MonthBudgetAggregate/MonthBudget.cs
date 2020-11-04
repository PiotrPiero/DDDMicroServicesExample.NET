using HomeBudget.Core.Impl;
using HomeBudget.MonthBudget.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate
{
    public class MonthBudget : AggregateRoot
    {
        private readonly HashSet<FinOperation> _finOperations;
        private readonly HashSet<BudgetPlan> _categoriesPlans;

        public MonthBudget() : base() { _finOperations ??= new HashSet<FinOperation>(); }

        protected MonthBudget(HashSet<BudgetPlan> categoriesPlans, DateTime monthStartDate)
        {
            categoriesPlans ??= new HashSet<BudgetPlan>();
            _finOperations ??= new HashSet<FinOperation>();

            _categoriesPlans = categoriesPlans;
            MonthStartDate = monthStartDate;
        }

        public DateTime MonthStartDate { get; private set; }
        public decimal Limit => CategoriesPlans.Select(x => x.Limit).Sum();
        public decimal CurrentIncomes => GetSum(FinOperationType.Income);
        public decimal CurrentExpenses => GetSum(FinOperationType.Expense);
        public virtual IReadOnlyCollection<FinOperation> FinOperations => _finOperations;
        public virtual IReadOnlyCollection<BudgetPlan> CategoriesPlans => _categoriesPlans;

        public void AddOperation(FinOperationValue operationValue, string accountName, FinOperationType type, DateTime operationDate, int authorId, BudgetCategory budgetCategory, string name, string description = null)
        {
            if (!budgetCategory.CanHaveIncomes) throw new ArgumentException(); //toDo errors
            //todo validation
            var newOperation = FinOperation.New(name, accountName, type, operationDate, budgetCategory, this, authorId, operationValue);
            newOperation.Description = description;


            _finOperations.Add(newOperation);

            DomainEvents.Add(new FinOperationAddedDomainEvent(newOperation.Id.Value, newOperation.MonthBudget.Id.Value, accountName));
        }

        public void RemoveOperation(int operationId)
        {
            _finOperations.Remove(FinOperations.First(x => x.Id.Equals(operationId)));

            //this.DomainEvents.Add(new FinOperationRemoved(operationId, this.Id.Value));
        }

        private decimal GetSum(FinOperationType type) => FinOperations.Where(x => x.Type.Equals(type)).Select(x => x.OperationValue.Gross).Sum();
    }
}
