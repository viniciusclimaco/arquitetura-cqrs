namespace EmergingBooking.Infrastructure.CQRS.Events
{
    public abstract class EventHandler<TEvent> : IEventHandler<TEvent>
        where TEvent : IEvent
    {
        public abstract Task HandleAsync(TEvent @event);
        
    }
}
