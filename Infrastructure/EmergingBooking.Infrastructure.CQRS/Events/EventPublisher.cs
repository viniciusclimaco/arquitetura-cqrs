using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingBooking.Infrastructure.CQRS.Events
{
    internal class EventPublisher : IEventPublisher
    {
        private readonly DependencyResolver _dependencyResolver;

        public EventPublisher(DependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (@event == null)
                throw new ArgumentNullException($"The {nameof(@event)} MUST to be created");

            var eventHandlers = _dependencyResolver.ResolveAll<IEventHandler<TEvent>>();
            foreach (var handler in eventHandlers)
            {
                await handler.HandleAsync(@event);
            }
        }
    }
}
