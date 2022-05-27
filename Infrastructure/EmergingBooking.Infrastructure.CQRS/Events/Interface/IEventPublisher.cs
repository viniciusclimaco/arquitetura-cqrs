namespace EmergingBooking.Infrastructure.CQRS.Events
{
    public interface IEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
