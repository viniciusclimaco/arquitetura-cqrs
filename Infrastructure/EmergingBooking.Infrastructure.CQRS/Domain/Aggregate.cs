using EmergingBooking.Infrastructure.CQRS.Events;

namespace EmergingBooking.Infrastructure.CQRS.Domain
{
    public class Aggregate : Entity
    {
        public Aggregate(Guid? identifier) : base(identifier)
        {

        }

        private readonly List<IEvent> _events = new List<IEvent>();
        public IReadOnlyList<IEvent> Events => _events;

        protected void AddEvent(IEvent @event)
        {
            _events.Add(@event);
        }
    }
}
