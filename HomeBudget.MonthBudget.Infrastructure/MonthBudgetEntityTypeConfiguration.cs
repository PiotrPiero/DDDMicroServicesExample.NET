using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonthBudgetDomain = HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate;

namespace HomeBudget.MonthBudget.Infrastructure
{
    public class MonthBudgetEntityTypeConfiguration : IEntityTypeConfiguration<MonthBudgetDomain.MonthBudget>
    {
        public void Configure(EntityTypeBuilder<MonthBudgetDomain.MonthBudget> builder)
        {
            builder.ToTable("MonthBudgets", MonthBudgetContext.DEFAULT_SCHEMA);

            builder
                .Ignore(x => x.CategoriesPlans)
                .Ignore(x => x.CurrentExpenses)
                .Ignore(x => x.CurrentIncomes)
                .Ignore(x => x.DomainEvents);
            //.Ignore(x => x.Limit);

            builder.HasMany(x => x.FinOperations).WithOne(x => x.MonthBudget);
            builder.HasMany(x => x.CategoriesPlans).WithOne(x => x.MonthBudget);

            var finOperationsNavigation =
                builder.Metadata.FindNavigation(nameof(MonthBudgetDomain.MonthBudget.FinOperations));
            finOperationsNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            var categoriesPlansNavigation =
                builder.Metadata.FindNavigation(nameof(MonthBudgetDomain.MonthBudget.CategoriesPlans));
            categoriesPlansNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}