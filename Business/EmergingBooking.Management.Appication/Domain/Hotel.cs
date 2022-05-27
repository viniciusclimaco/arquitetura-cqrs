using EmergingBooking.Infrastructure.CQRS.Domain;
using EmergingBooking.Management.Appication.Domain.Events;

namespace EmergingBooking.Management.Appication.Domain
{
    internal class Hotel : Aggregate
    {   
        private IList<Room> _rooms;
        public Hotel(string name, 
                     int startsOfCategory, 
                     Address address, 
                     Contacts contacts, 
                     Guid code = default(Guid), 
                     Guid? identifier = null) : base(identifier)
        {
            Name = name;
            StartsOfCategory = startsOfCategory;
            Address = address;
            StartsOfRating = 0;
            Contacts = contacts;
            Code = code;            

            _rooms = new List<Room>();
            Rooms = new List<Room>();

            if (Code == default(Guid))
            {
                Code = Guid.NewGuid();

                AddEvent(new HotelCreated(Code, Name, StartsOfCategory, StartsOfRating, Address, Contacts));
            }
        }

        public string Name { get; }
        public int StartsOfCategory { get; }
        public int StartsOfRating { get; }
        public Address Address { get; private set; }
        public Contacts Contacts { get; private set; }
        public Guid Code { get; }

        public IEnumerable<Room> Rooms {
            get => _rooms.ToList();
            set => _rooms = value.ToList();
        }

        public void ChangeAddress(Address address)
        {
            Address = address;

            AddEvent(new HotelAddressUpdated(this.Code, address.Street, address.District, address.City, address.Country, address.ZipCode));
        }

        public void ChangeContacts(Contacts contacts)
        {
            Contacts = contacts;

            AddEvent(new HotelContactUpdated(this.Code, contacts.Email, contacts.Phone, contacts.Mobile));
        }

        public void AddRoom(Room room)
        {
            if (_rooms.Contains(room))            
                throw new InvalidOperationException($"The room {room.Name} already was added.");

            _rooms.Add(room);

            AddEvent(new RoomAdded(room.Code, this.Code, room.Name, room.Description, room.PricePerNight, room.Capacity, room.AvailableQuantity, room.Amenities));
        }

        public void RemoveRoom(Room room)
        {
            if (!_rooms.Contains(room))
                throw new InvalidOperationException($"The room {room.Name} doesn´t exists.");

            _rooms.Remove(room);
        }
    }
}
