namespace EmergingBooking.Infrastructure.CQRS.Events
{
    public class EventBase : IEvent
    {
        public EventBase(string eventName, string version)
        {
            EventName = eventName;
            Version = version;
            OccuredAt = DateTime.Now;
        }

        public Guid EventId => Guid.NewGuid();

        public string EventName { get; }
        public string Version { get; }
        public DateTime OccuredAt { get; }
    }
}
