using EmergingBooking.Infrastructure.CQRS.Command;
using EmergingBooking.Management.Appication.Commands;
using EmergingBooking.Management.Appication.Domain;
using EmergingBooking.Management.Appication.Repository;
using MonoidSharp;

namespace EmergingBooking.Management.Appication.Handlers
{
    internal class CreateHotelHandler : ICommandHandler<CreateHotel>
    {
        private readonly HotelPersistence _hotelPersistence;

        public CreateHotelHandler(HotelPersistence hotelPersistence)
        {
            _hotelPersistence = hotelPersistence;
        }
        public async Task<CommandResult> ExecuteAsync(CreateHotel command)
        {
            try
            {
                var address = Address.Create(command.Street, command.District, command.City, command.Country, command.ZipCode);
                var contacts = Contacts.Create(command.Email, command.Mobile, command.Phone);

                var domainCombinedValues = Outcome.Combine(address, contacts);
                if (domainCombinedValues.Failure)
                    return CommandResult.Fail(domainCombinedValues.ErrorMessages);

                var hotel = new Hotel(command.Name, command.StarsOfCategory, address.Value, contacts.Value);
                await _hotelPersistence.CreateHotel(hotel);
                return CommandResult.Ok();
            }
            catch (Exception)
            {
                return CommandResult.Fail($"Error while creating the Hotel {command.Name}");
            }            
        }
    }
}
