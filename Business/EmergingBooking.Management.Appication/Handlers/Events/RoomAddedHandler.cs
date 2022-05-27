using EmergingBooking.Infrastructure.CQRS.Events;
using EmergingBooking.Infrastructure.KafkaProducer;
using EmergingBooking.Management.Appication.Domain.Events;
using EmergingBooking.Management.Appication.Settings;
using Microsoft.Extensions.Options;

namespace EmergingBooking.Management.Appication.Handlers.Events
{
    internal class RoomAddedHandler : IEventHandler<RoomAdded>
    {
        private readonly ManagementProducerSettings _managementOptions;

        public RoomAddedHandler(IOptions<ManagementProducerSettings> managementOptions)
        {
            _managementOptions = managementOptions.Value;
        }

        public async Task HandleAsync(RoomAdded @event)
        {
            try
            {
                using (var producer = new KafkaProducer<string, RoomAdded>(_managementOptions.TopicName, _managementOptions.Server))
                {
                    await producer.ProduceMessage(@event, @event.PartitionKey());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }        
    }
}
