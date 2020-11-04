using Microsoft.EntityFrameworkCore;
using MonthBudgetDomain = HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate;
namespace HomeBudget.MonthBudget.Infrastructure
{
    public class MonthBudgetContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";

        public MonthBudgetContext(DbContextOptions<MonthBudgetContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<MonthBudgetDomain.MonthBudget>(new MonthBudgetEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration<MonthBudgetDomain.FinOperation>(new FinOperationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration<MonthBudgetDomain.BudgetPlan>(new BudgetPlanEntityTypeConfiguration());
        }

        public DbSet<MonthBudgetDomain.MonthBudget> MonthBudgets { get; private set; }
        public DbSet<MonthBudgetDomain.FinOperation> FinOperations { get; private set; }
        public DbSet<MonthBudgetDomain.BudgetCategory> BudgetCategories { get; private set; }
        public DbSet<MonthBudgetDomain.BudgetPlan> BudgetPlans { get; private set; }
    }
}
