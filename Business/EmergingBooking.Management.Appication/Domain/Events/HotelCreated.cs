namespace EmergingBooking.Management.Appication.Domain.Events
{
    internal class HotelCreated : HotelEventBaseV1
    {
        public HotelCreated(Guid code, string name, int starsOfCategory, int startsOfRating, Address address, Contacts contacts) : base(nameof(HotelCreated))
        {
            Code = code;
            Name = name;
            StarsOfCategory = starsOfCategory;
            StarsOfRating = startsOfRating;
            Address = address;
            Contacts = contacts;
        }

        private readonly Address Address;
        private readonly Contacts Contacts;

        public Guid Code { get; }
        public string Name { get; }
        public int StarsOfCategory { get; }
        public int StarsOfRating { get; }
        public string Street => Address.Street;
        public string District => Address.District;
        public string City => Address.City;
        public string Country => Address.Country;
        public int ZipCode => Address.ZipCode;
        public string Email => Contacts.Email;
        public string Phone => Contacts.Phone;
        public string Mobile => Contacts.Mobile;

    }
}
