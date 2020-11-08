using HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate;
using HomeBudget.MonthBudget.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using HomeBudget.Integration;
using MediatR;

namespace HomeBudget.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        MonthBudgetContext _ctx;
        private readonly IMediator _mediator;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, MonthBudgetContext ctx, IMediator mediator)
        {
            _logger = logger;
            _ctx = ctx;
            _mediator = mediator;
        }

        [HttpGet]
        public IEnumerable<MonthBudget.Domain.Aggregates.MonthBudgetAggregate.MonthBudget> Get()
        {
            //var r = _ctx.MonthBudgets
            //    .Include(x => x.FinOperations).ThenInclude(o => o.BudgetCategory)
            //    .Include(x => x.CategoriesPlans).ThenInclude(o => o.BudgetCategory).ToList();

            var m = _ctx.MonthBudgets.Include(x => x.FinOperations).First();

            // _ctx.Attach(m);

            m.AddOperation(new FinOperationValue(100, 77), "Główne",FinOperationType.Expense, DateTime.Now, 1, _ctx.BudgetCategories.First(), "testowy wydatek 1", "opis");

            _mediator.DispatchDomainEventsAsync(_ctx);
            return null;
        }
    }
}
