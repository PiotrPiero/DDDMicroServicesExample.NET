using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AccountDomain = HomeBudget.Account.Domain.Aggregates.AccountAggregate;

namespace HomeBudget.Account.Infrastructure
{
    public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<AccountDomain.Account>
    {
        public void Configure(EntityTypeBuilder<AccountDomain.Account> builder)
        {
            builder.ToTable("Accounts", AccountContext.DEFAULT_SCHEMA);

            builder
                .Ignore(x => x.DomainEvents);

        }
    }
}