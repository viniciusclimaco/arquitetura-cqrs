using EmergingBooking.Infrastructure.CQRS.Events;
using EmergingBooking.Infrastructure.RavenDB.Interface;
using EmergingBooking.Management.Appication.Domain;
using Raven.Client.Documents;
using Raven.Client.Documents.Commands.Batches;
using Raven.Client.Documents.Operations;

namespace EmergingBooking.Management.Appication.Repository
{
    internal class HotelPersistence
    {
        private readonly IRavenDocumentStoreHolder _ravenDocumentStoreHolder;
        private readonly IEventPublisher _eventPublisher;
        public HotelPersistence(IEventPublisher eventPublisher, 
                                IRavenDocumentStoreHolder ravenDocumentStoreHolder)
        {
            _eventPublisher = eventPublisher;
            _ravenDocumentStoreHolder = ravenDocumentStoreHolder;
        }

        internal async Task CreateHotel(Hotel hotel)
        {
            using (var session = _ravenDocumentStoreHolder.Store.OpenAsyncSession())
            {
                await session.StoreAsync(hotel, hotel.Identifier.ToString());
                await session.SaveChangesAsync();

                foreach (var @event in hotel.Events)
                {
                    await _eventPublisher.PublishAsync((dynamic)@event);
                }
            }
        }

        internal async Task UpdateHotelAddress(Hotel hotel)
        {
            using (var session = _ravenDocumentStoreHolder.Store.OpenAsyncSession())
            {
                session.Advanced.Defer(new PatchCommandData(
                    hotel.Identifier.ToString(),
                    null,
                    new PatchRequest
                    {
                        Script = $"this.{nameof(hotel.Address)} = args.{nameof(hotel.Address)};",
                        Values =
                        {
                            { $"{nameof(hotel.Address)}", hotel.Address }
                        }
                    }), null);
                await session.SaveChangesAsync();

                foreach (var @event in hotel.Events)
                {
                    await _eventPublisher.PublishAsync((dynamic)@event);
                }
            }
        }

        internal async Task UpdateHotelContacts(Hotel hotel)
        {
            using (var session = _ravenDocumentStoreHolder.Store.OpenAsyncSession())
            {
                session.Advanced.Defer(new PatchCommandData(
                    hotel.Identifier.ToString(),
                    null,
                    new PatchRequest
                    {
                        Script = $"this.{nameof(hotel.Contacts)} = args.{nameof(hotel.Contacts)};",
                        Values =
                        {
                            { $"{nameof(hotel.Contacts)}", hotel.Contacts }
                        }
                    }), null);
                await session.SaveChangesAsync();

                foreach (var @event in hotel.Events)
                {
                    await _eventPublisher.PublishAsync((dynamic)@event);
                }
            }
        }

        internal async Task AddRoomHotel(Hotel hotel)
        {
            using (var session = _ravenDocumentStoreHolder.Store.OpenAsyncSession())
            {
                session.Advanced.Defer(new PatchCommandData(
                    hotel.Identifier.ToString(),
                    null,
                    new PatchRequest
                    {
                        Script = $"this.{nameof(hotel.Rooms)}.push(args.Rooms);",
                        Values =
                        {
                            { $"Room", hotel.Rooms.ElementAt(hotel.Rooms.Count() - 1) }
                        }
                    }), null);
                await session.SaveChangesAsync();

                foreach (var @event in hotel.Events)
                {
                    await _eventPublisher.PublishAsync((dynamic)@event);
                }
            }
        }

        internal async Task<Hotel> RetrieveHotelByCodeAsync(Guid hotelCode)
        {
            using (var session = _ravenDocumentStoreHolder.Store.OpenAsyncSession())
            {
                var searchedHotel = await session.Query<Hotel>().Where(hotel => hotel.Code == hotelCode).FirstOrDefaultAsync();
                return searchedHotel;
            }
        }

    }
}
