using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonthBudgetDomain = HomeBudget.MonthBudget.Domain.Aggregates.MonthBudgetAggregate;

namespace HomeBudget.MonthBudget.Infrastructure
{
    public class FinOperationEntityTypeConfiguration : IEntityTypeConfiguration<MonthBudgetDomain.FinOperation>
    {
        public void Configure(EntityTypeBuilder<MonthBudgetDomain.FinOperation> builder)
        {
            builder.ToTable("FinOperations", MonthBudgetContext.DEFAULT_SCHEMA);

            builder.OwnsOne(x => x.OperationValue, t =>
            {
                t.Property(x => x.Gross).HasColumnName("Gross");
                t.Property(x => x.Net).HasColumnName("Net");
            });


            builder
                .HasOne(x => x.BudgetCategory)
                .WithMany(x => x.SavedOperations);

        }
    }
}
