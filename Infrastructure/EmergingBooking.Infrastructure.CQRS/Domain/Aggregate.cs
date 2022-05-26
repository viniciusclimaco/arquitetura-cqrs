using EmergingBooking.Infrastructure.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
