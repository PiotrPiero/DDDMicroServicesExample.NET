using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonthBudgetDomain = HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate;

namespace HomeBudget.MonthBudget.Infrastructure
{
    public class BudgetPlanEntityTypeConfiguration : IEntityTypeConfiguration<MonthBudgetDomain.BudgetPlan>
    {
        public void Configure(EntityTypeBuilder<MonthBudgetDomain.BudgetPlan> builder)
        {
            builder.ToTable("BudgetPlans", MonthBudgetContext.DEFAULT_SCHEMA);

            builder
                .HasOne(x => x.BudgetCategory);
            builder
                .HasOne(x => x.MonthBudget).WithMany(x => x.CategoriesPlans);

        }
    }
}
