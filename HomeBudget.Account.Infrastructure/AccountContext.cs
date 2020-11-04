using Microsoft.EntityFrameworkCore;
using AccountDomain = HomeBudget.Account.Domain.Aggregates.AccountAggregate;
using TransferDomain = HomeBudget.Account.Domain.Aggregates.TransferAggregate;
namespace HomeBudget.Account.Infrastructure
{
    public class AccountContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";

        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<AccountDomain.Account>(new AccountEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration<TransferDomain.Transfer>(new TransferEntityTypeConfiguration());

        }

        public DbSet<AccountDomain.Account> Accounts { get; set; }
        public DbSet<TransferDomain.Transfer> Transfers { get; set; }
    }
}
