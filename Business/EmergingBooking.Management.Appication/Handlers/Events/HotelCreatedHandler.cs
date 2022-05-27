using EmergingBooking.Infrastructure.CQRS.Events;
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
    internal class HotelCreatedHandler : IEventHandler<HotelCreated>
    {
        private readonly ManagementProducerSettings _managementOptions;
        public HotelCreatedHandler(IOptions<ManagementProducerSettings> options)
        {
            _managementOptions = options.Value;
        }

        public async Task HandleAsync(HotelCreated @event)
        {
            try
            {
                using (var producer = new KafkaProducer<string, HotelCreated>(_managementOptions.TopicName, _managementOptions.Server))
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
