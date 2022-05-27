using EmergingBooking.Infrastructure.CQRS.Command;

namespace EmergingBooking.Management.Appication.Commands
{
    public class UpdateHotelAddress : ICommand
    {        
        public UpdateHotelAddress(Guid hotelCode, string newStreet, string newDistrict, string newCity, string newCountry, int newZipCode)
        {
            HotelCode = hotelCode;
            NewStreet = newStreet;
            NewDistrict = newDistrict;
            NewCity = newCity;
            NewCountry = newCountry;
            NewZipCode = newZipCode;
        }

        public Guid HotelCode { get; }
        public string NewStreet { get; }
        public string NewDistrict { get; }
        public string NewCity { get; }
        public string NewCountry { get; }
        public int NewZipCode { get; }
    }
}
