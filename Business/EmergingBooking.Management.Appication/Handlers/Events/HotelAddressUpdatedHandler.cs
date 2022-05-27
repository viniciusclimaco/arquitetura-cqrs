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
    internal class HotelAddressUpdatedHandler : Infrastructure.CQRS.Events.IEventHandler<HotelAddressUpdated>
    {
        private readonly ManagementProducerSettings _managementOptions;

        public HotelAddressUpdatedHandler(IOptions<ManagementProducerSettings> managementOptions)
        {
            _managementOptions = managementOptions.Value;
        }

        public async Task HandleAsync(HotelAddressUpdated @event)
        {
            try
            {
                using (var producer = new KafkaProducer<string, HotelAddressUpdated>(_managementOptions.TopicName, _managementOptions.Server))
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
