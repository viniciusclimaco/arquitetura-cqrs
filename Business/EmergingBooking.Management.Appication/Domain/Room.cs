namespace EmergingBooking.Management.Appication.Domain
{
    internal class Room
    {
        private readonly IList<string> _amenities;

        public Room(string name, string description, int capacity, int availableQuantity, decimal pricePerNight, Guid? code = null)
        {
            Name = name;
            Description = description;
            Capacity = capacity;
            AvailableQuantity = availableQuantity;
            PricePerNight = pricePerNight;
            Code = code ?? Guid.NewGuid();
        }

        public string Name { get; }

        public string Description { get; }
        public int Capacity { get; }
        public int AvailableQuantity { get; }
        public decimal PricePerNight { get; }
        public Guid Code { get; }

        public IReadOnlyList<string> Amenities => _amenities.ToList();

        public void AddAmenities(string amenity)
        {
            if (_amenities.Contains(amenity))            
                throw new InvalidOperationException($"The amenity {amenity} was already added.");
            
            _amenities.Add(amenity);
        }
    }
}
