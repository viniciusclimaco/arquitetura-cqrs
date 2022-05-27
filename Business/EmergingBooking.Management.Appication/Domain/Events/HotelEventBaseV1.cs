﻿using EmergingBooking.Infrastructure.CQRS.Events;

namespace EmergingBooking.Management.Appication.Domain.Events
{
    internal class HotelEventBaseV1 : EventBase
    {
        public HotelEventBaseV1(string eventName) : base(eventName, "1.0")
        {

        }

        public string PartitionKey() => this.EventId.ToString();
    }    
}
