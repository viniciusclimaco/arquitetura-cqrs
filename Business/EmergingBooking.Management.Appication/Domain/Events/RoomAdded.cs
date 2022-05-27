namespace EmergingBooking.Management.Appication.Domain.Events
{
    internal sealed class RoomAdded : HotelEventBaseV1
    {
        public RoomAdded(Guid hotelCode, Guid code, string name, string description, decimal pricePerNight, int capacity, int availableQuantity, IReadOnlyList<string> amenities) : base(nameof(RoomAdded))
        {
            HotelCode = hotelCode;
            Code = code;
            Name = name;
            Description = description;
            PricePerNight = pricePerNight;
            Capacity = capacity;
            AvailableQuantity = availableQuantity;
            Amenities = amenities;
        }

        public Guid HotelCode { get; }
        public Guid Code { get; }

        public string Name { get; }
        public string Description { get; }
        public decimal PricePerNight { get; }
        public int Capacity { get; }
        public int AvailableQuantity { get; }
        public IReadOnlyList<string> Amenities { get; }

    }
}
