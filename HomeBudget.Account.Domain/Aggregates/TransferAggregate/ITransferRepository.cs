namespace HomeBudget.Account.Domain.Aggregates.TransferAggregate
{
    public interface ITransferRepository
    {
        public void Add(Transfer transfer);
        public void GetTransfersForAccount(AccountId id);
    }
}
