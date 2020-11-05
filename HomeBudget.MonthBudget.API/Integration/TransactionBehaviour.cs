using System;
using System.Threading;
using System.Threading.Tasks;
using HomeBudget.Integration;
using HomeBudget.MonthBudget.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeBudget.MonthBudget.API.Integration
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        private readonly MonthBudgetContext _ctx;
        private readonly IIntegrationService _monthBudgetIntegrationService;

        public TransactionBehaviour(MonthBudgetContext ctx,
            IIntegrationService monthBudgetIntegrationService,
            ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _ctx = ctx ?? throw new ArgumentException(nameof(MonthBudgetContext));
            _monthBudgetIntegrationService = monthBudgetIntegrationService ?? throw new ArgumentException(nameof(monthBudgetIntegrationService));
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var typeName = request.GetType().GetGenericTypeDefinition().Name;

            try
            {
                if (_ctx.Database.CurrentTransaction != null)
                {
                    return await next();
                }

                var strategy = _ctx.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;

                    using (var transaction = await _ctx.Database.BeginTransactionAsync())
                    {
                        _logger.LogInformation(
                            $"Begin transaction {transaction.TransactionId} for {typeName} ({request})");

                        response = await next();

                        _logger.LogInformation($"Commit transaction {transaction.TransactionId} for {typeName}");

                        await _ctx.Database.CommitTransactionAsync();

                        transactionId = transaction.TransactionId;
                    }

                    await _monthBudgetIntegrationService.PublishEventsThroughEventBusAsync(transactionId);
                    
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in transaction for {typeName} ({request})");

                throw;
            }
        }
    }
}