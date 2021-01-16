using HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate;
using HomeBudget.MonthBudget.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeBudget.Integration;
using HomeBudget.MonthBudget.API.Commands;
using MediatR;

namespace HomeBudget.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonthBudgetController : ControllerBase
    {
        private readonly IMediator _mediator;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<MonthBudgetController> _logger;

        public MonthBudgetController(ILogger<MonthBudgetController> logger, MonthBudgetContext ctx, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<MonthBudget.Domain.Aggregates.MonthBudgetAggregate.MonthBudget>> Get()
        {
            var command = new AddFinOperationCommand()
            {
                Value = new FinOperationValue(100, 77),
                AccountName = "Główne",
                Type = FinOperationType.Expense,
                AuthorId = 1,
                BudgetCategoryId = 1,
                Name = "a1",
                Description = "opis2",
                Created = DateTime.Now,
                MonthBudgetId = 1
            };

            await _mediator.Send(command);
            return null;
        }
    }
}
