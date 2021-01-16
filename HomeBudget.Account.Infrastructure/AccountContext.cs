using System.Threading;
using System.Threading.Tasks;
using HomeBudget.Integration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AccountDomain = HomeBudget.Account.Domain.Aggregates.AccountAggregate;
using TransferDomain = HomeBudget.Account.Domain.Aggregates.TransferAggregate;
namespace HomeBudget.Account.Infrastructure
{
    public class AccountContext : DbContext
    {
        private readonly IMediator _mediator;
        public const string DEFAULT_SCHEMA = "dbo";

        public AccountContext(DbContextOptions<AccountContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<AccountDomain.Account>(new AccountEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration<TransferDomain.Transfer>(new TransferEntityTypeConfiguration());

        }

        public DbSet<AccountDomain.Account> Accounts { get; set; }
        public DbSet<TransferDomain.Transfer> Transfers { get; set; }
        
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            var domainEvents =  _mediator.GetDomainEvents(this);
            var res = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await _mediator.DispatchDomainEventsAsync(domainEvents);

            return res;
        }
    }
    
}
