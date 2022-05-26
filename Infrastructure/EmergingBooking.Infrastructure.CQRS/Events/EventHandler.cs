using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingBooking.Infrastructure.CQRS.Events
{
    public abstract class EventHandler<TEvent> : IEventHandler<TEvent>
        where TEvent : IEvent
    {
        public abstract Task HandleAsync(TEvent @event);
        
    }
}
