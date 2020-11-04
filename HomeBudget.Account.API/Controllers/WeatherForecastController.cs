using System.Collections.Generic;
using System.Linq;
using HomeBudget.Account.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeBudget.Account.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        AccountContext _ctx;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, AccountContext ctx)
        {
            _logger = logger;
            _ctx = ctx;


        }

        [HttpGet]
        public IEnumerable<Domain.Aggregates.AccountAggregate.Account> Get()
        {
            var res = _ctx.Accounts.ToList();

            return res;
        }
    }
}
