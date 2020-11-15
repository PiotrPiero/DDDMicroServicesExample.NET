using System;
using HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate;
using MediatR;

namespace HomeBudget.MonthBudget.API.Commands
{
    public class AddFinOperationCommand: IRequest //todo webserviceresult
    {
        //todo refactor!
        public int MonthBudgetId { get; set; }
        public FinOperationValue Value { get; set;  }
        public string AccountName { get; set; }
        public FinOperationType Type { get; set; }
        public int AuthorId { get; set; }
        public int BudgetCategoryId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
    }
}