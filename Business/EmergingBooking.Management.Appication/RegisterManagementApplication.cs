using EmergingBooking.Infrastructure.CQRS.Command;
using EmergingBooking.Infrastructure.CQRS.Events;
using EmergingBooking.Infrastructure.RavenDB;
using EmergingBooking.Management.Appication.Commands;
using EmergingBooking.Management.Appication.Domain.Events;
using EmergingBooking.Management.Appication.Handlers;
using EmergingBooking.Management.Appication.Handlers.Events;
using EmergingBooking.Management.Appication.Repository;
using EmergingBooking.Management.Appication.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmergingBooking.Management.Appication
{
    public static class RegisterManagementApplication
    {
        public static IServiceCollection RegisterManagementApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ManagementProducerSettings>(setting =>
            {
                configuration.GetSection(nameof(ManagementProducerSettings)).Bind(setting);
            });

            services.RegisterRavenDbStoreStorageInfraDependencies(configuration);

            services.AddTransient<ICommandHandler<CreateHotel>, CreateHotelHandler>();
            services.AddTransient<ICommandHandler<UpdateHotelAddress>, UpdateHotelAddressHandler>();
            services.AddTransient<ICommandHandler<UpdateHotelContacts>, UpdateHotelContactHandler>();
            services.AddTransient<ICommandHandler<AddRoomHotel>, AddRoomHotelHandler>();

            services.AddTransient<IEventHandler<HotelCreated>, HotelCreatedHandler>();
            services.AddTransient<IEventHandler<HotelAddressUpdated>, HotelAddressUpdatedHandler>();
            services.AddTransient<IEventHandler<HotelContactUpdated>, HotelContactUpdatedHandler>();
            services.AddTransient<IEventHandler<RoomAdded>, RoomAddedHandler>();

            services.AddTransient<HotelPersistence, HotelPersistence>();

            return services;
        }
    }
}
