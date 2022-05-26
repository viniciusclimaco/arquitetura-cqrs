using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingBooking.Infrastructure.CQRS.Events
{
    public interface IEventHandler <in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
