using System.Threading.Tasks;

namespace HomeBudget.Integration
{
    public interface IIntegrationEventHandler<T> where T: IIntegrationEvent
    {
        Task Handle(T @event);
    }
}