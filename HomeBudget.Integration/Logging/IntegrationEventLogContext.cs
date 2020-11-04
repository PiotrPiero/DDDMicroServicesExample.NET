using Microsoft.EntityFrameworkCore;

namespace HomeBudget.Integration.Logging
{
    public class IntegrationEventLogContext: DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";
        
        public IntegrationEventLogContext(DbContextOptions<IntegrationEventLogContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<IntegrationEventLog>(new IntegrationEventLogEntityTypeConfiguration());
        }

        public DbSet<IntegrationEventLog> IntegrationEventLogs { get; set; }
    }
}