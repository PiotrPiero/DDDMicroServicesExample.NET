using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeBudget.Integration.Logging
{
    public class IntegrationEventLogEntityTypeConfiguration : IEntityTypeConfiguration<IntegrationEventLog>
    {
        public void Configure(EntityTypeBuilder<IntegrationEventLog> builder)
        {
            builder.ToTable("IntegrationEventLogs", IntegrationEventLogContext.DEFAULT_SCHEMA);

            builder
                .Ignore(x => x.Event);
        }
    }
}