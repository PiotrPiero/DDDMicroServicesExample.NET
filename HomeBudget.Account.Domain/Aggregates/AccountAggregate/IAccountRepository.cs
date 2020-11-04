namespace HomeBudget.Account.Domain.Aggregates.AccountAggregate
{
    public interface IAccountRepository
    {
        void Add(Account account);
        void Save(Account account);

    }
}
