using EmergingBooking.Infrastructure.CQRS.Command;
using EmergingBooking.Management.Appication.Commands;
using EmergingBooking.Management.Appication.Domain;
using EmergingBooking.Management.Appication.Repository;
using MonoidSharp;

namespace EmergingBooking.Management.Appication.Handlers
{
    internal class UpdateHotelContactHandler : ICommandHandler<UpdateHotelContacts>
    {
        private readonly HotelPersistence _hotelPersistence;
        public UpdateHotelContactHandler(HotelPersistence hotelPersistence)
        {
            _hotelPersistence = hotelPersistence;
        }
        public async Task<CommandResult> ExecuteAsync(UpdateHotelContacts command)
        {
            try
            {
                var hotel = await _hotelPersistence.RetrieveHotelByCodeAsync(command.HotelCode);
                if (hotel == null)
                    return CommandResult.Fail("There isn´t an hotel to update the Contact");

                var contact = Contacts.Create(command.NewEmail, command.NewMobile, command.NewPhone) ;
                var domainCombinedValues = Outcome.Combine(contact);

                if (domainCombinedValues.Failure)
                    return CommandResult.Fail(domainCombinedValues.ErrorMessages);

                hotel.ChangeContacts(contact.Value);
                await _hotelPersistence.UpdateHotelContacts(hotel);
                return CommandResult.Ok();
            }
            catch (Exception)
            {
                return CommandResult.Fail("Error while updating the contacts for the hotel.");
            }
        }
    }
}
