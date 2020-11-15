using HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeBudget.MonthBudget.Infrastructure
{
    public class BudgetCategoryEntityTypeConfiguration : IEntityTypeConfiguration<BudgetCategory>
    {
        public void Configure(EntityTypeBuilder<BudgetCategory> builder)
        {
            builder.ToTable("BudgetCategories", MonthBudgetContext.DEFAULT_SCHEMA);
        

        }
    }
}