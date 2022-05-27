namespace EmergingBooking.Management.Appication.Domain.Events
{
    internal sealed class HotelContactUpdated : HotelEventBaseV1
    {
        public HotelContactUpdated(Guid hotelCode, string email, string phone, string mobile) : base(nameof(HotelContactUpdated))
        {
            HotelCode = hotelCode;
            Email = email;
            Phone = phone;
            Mobile = mobile;            
        }

        public Guid HotelCode { get; }
        public string Email { get; }
        public string Phone { get; }
        public string Mobile { get; }
    }
}
