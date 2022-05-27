using EmergingBooking.Infrastructure.CQRS.Command;
using EmergingBooking.Management.Appication.Commands;
using EmergingBooking.Management.Appication.Domain;
using EmergingBooking.Management.Appication.Repository;
using MonoidSharp;

namespace EmergingBooking.Management.Appication.Handlers
{
    internal class UpdateHotelAddressHandler : ICommandHandler<UpdateHotelAddress>
    {
        private readonly HotelPersistence _hotelPersistence;
        public UpdateHotelAddressHandler(HotelPersistence hotelPersistence)
        {
            _hotelPersistence = hotelPersistence;
        }

        public async Task<CommandResult> ExecuteAsync(UpdateHotelAddress command)
        {
            try
            {
                var hotel = await _hotelPersistence.RetrieveHotelByCodeAsync(command.HotelCode);
                if (hotel == null)
                    return CommandResult.Fail("There isn´t an hotel to update the Address");

                var address = Address.Create(command.NewStreet, command.NewDistrict, command.NewCity, command.NewCountry, command.NewZipCode);
                var domainCombinedValues = Outcome.Combine(address);
                
                if (domainCombinedValues.Failure)
                    return CommandResult.Fail(domainCombinedValues.ErrorMessages);

                hotel.ChangeAddress(address.Value);
                await _hotelPersistence.UpdateHotelAddress(hotel);
                return CommandResult.Ok();
            }
            catch (Exception)
            {
                return CommandResult.Fail("Error while updating the address for the hotel.");
            }
        }
    }
}
