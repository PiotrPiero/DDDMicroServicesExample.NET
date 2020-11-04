using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransferDomain = HomeBudget.Account.Domain.Aggregates.TransferAggregate;

namespace HomeBudget.Account.Infrastructure
{
    public class TransferEntityTypeConfiguration : IEntityTypeConfiguration<TransferDomain.Transfer>
    {
        public void Configure(EntityTypeBuilder<TransferDomain.Transfer> builder)
        {
            builder.ToTable("Transfers", AccountContext.DEFAULT_SCHEMA);

            builder.OwnsOne<TransferDomain.AccountId>(x => x.Receiver,
                receiver =>
            {
                receiver.Property(x => x.Id).HasColumnName("Receiver");
            });
        
            builder.OwnsOne<TransferDomain.AccountId>(x => x.Sender,
                receiver =>
            {
                receiver.Property(x => x.Id).HasColumnName("Sender");
            });

            builder.OwnsOne(x => x.Value, value =>
            {
                value.Property(x => x.Value).HasColumnName("Value");
                value.Property(x => x.Currency).HasColumnName("Currency");
            });
        }
    }
}
