using System;
using System.Threading;
using System.Threading.Tasks;
using HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate;
using MediatR;

namespace HomeBudget.MonthBudget.API.Commands
{
    public class AddFinOperationCommandHandler: IRequestHandler<AddFinOperationCommand>
    {
        private readonly IMonthBudgetRepository _repository;

        public AddFinOperationCommandHandler(IMonthBudgetRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<Unit> Handle(AddFinOperationCommand request, CancellationToken cancellationToken)
        {
            var m = await _repository.GetById(request.MonthBudgetId);
            var budgetCategory = await _repository.GetBudgetCategoryById(request.BudgetCategoryId);
            
            m.AddOperation(new FinOperationValue(100, 77), "Główne",FinOperationType.Expense, DateTime.Now, 1, budgetCategory, "testowy wydatek 1", "opis");

            return Unit.Value;
        }
    }
}