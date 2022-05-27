using EmergingBooking.Infrastructure.CQRS.Command;

namespace EmergingBooking.Management.Appication.Commands
{
    public class UpdateHotelContacts : ICommand
    {
        public UpdateHotelContacts(Guid hotelCode, string newPhone, string newEmail, string newMobile)
        {
            HotelCode = hotelCode;
            NewPhone = newPhone;
            NewEmail = newEmail;
            NewMobile = newMobile;
        }

        public Guid HotelCode { get; }
        public string NewPhone { get; }
        public string NewEmail { get; }
        public string NewMobile { get; }
        
    }
}
