using System.Threading;
using System.Threading.Tasks;
using HomeBudget.Integration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonthBudgetDomain = HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate;
namespace HomeBudget.MonthBudget.Infrastructure
{
    public class MonthBudgetContext : DbContext
    {
        private readonly IMediator _mediator;
        public const string DEFAULT_SCHEMA = "dbo";

        public MonthBudgetContext(DbContextOptions<MonthBudgetContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<MonthBudgetDomain.MonthBudget>(new MonthBudgetEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration<MonthBudgetDomain.FinOperation>(new FinOperationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration<MonthBudgetDomain.BudgetPlan>(new BudgetPlanEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration<MonthBudgetDomain.BudgetCategory>(new BudgetCategoryEntityTypeConfiguration());
        }

        public DbSet<MonthBudgetDomain.MonthBudget> MonthBudgets { get; private set; }
        public DbSet<MonthBudgetDomain.FinOperation> FinOperations { get; private set; }
        public DbSet<MonthBudgetDomain.BudgetCategory> BudgetCategories { get; private set; }
        public DbSet<MonthBudgetDomain.BudgetPlan> BudgetPlans { get; private set; }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            var domainEvents =  _mediator.GetDomainEvents(this);
            var res = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await _mediator.DispatchDomainEventsAsync(domainEvents);

            return res;
        }

    }
}
