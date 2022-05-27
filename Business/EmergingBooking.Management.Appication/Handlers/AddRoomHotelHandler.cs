using EmergingBooking.Infrastructure.CQRS.Command;
using EmergingBooking.Management.Appication.Commands;
using EmergingBooking.Management.Appication.Domain;
using EmergingBooking.Management.Appication.Repository;

namespace EmergingBooking.Management.Appication.Handlers
{
    internal class AddRoomHotelHandler : ICommandHandler<AddRoomHotel>
    {
        private readonly HotelPersistence _hotelPersistence;
        public AddRoomHotelHandler(HotelPersistence hotelPersistence)
        {
            _hotelPersistence = hotelPersistence;
        }
        public async Task<CommandResult> ExecuteAsync(AddRoomHotel command)
        {
            try
            {
                var hotel = await _hotelPersistence.RetrieveHotelByCodeAsync(command.HotelCode);
                if (hotel == null)
                    return CommandResult.Fail("There isn´t an hotel to add new room");

                var room = new Room(command.Name, command.Description, command.Capacity, command.AvailableQuantity, command.PricePerNight);

                foreach (var amenity in command.Amenities)
                {
                    room.AddAmenities(amenity);
                }

                hotel.AddRoom(room);
                await _hotelPersistence.AddRoomHotel(hotel);
                return CommandResult.Ok();
            }
            catch (Exception)
            {
                return CommandResult.Fail("Error while adding room for the hotel.");
            }
        }
    }
}
