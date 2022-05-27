using EmergingBooking.Infrastructure.KafkaProducer;
using EmergingBooking.Management.Appication.Domain.Events;
using EmergingBooking.Management.Appication.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingBooking.Management.Appication.Handlers.Events
{
    internal class HotelContactUpdatedHandler : Infrastructure.CQRS.Events.IEventHandler<HotelContactUpdated>
    {
        private readonly ManagementProducerSettings _managementOptions;

        public HotelContactUpdatedHandler(IOptions<ManagementProducerSettings> managementOptions)
        {
            _managementOptions = managementOptions.Value;
        }

        public async Task HandleAsync(HotelContactUpdated @event)
        {
            try
            {
                using (var producer = new KafkaProducer<string, HotelContactUpdated>(_managementOptions.TopicName, _managementOptions.Server))
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
