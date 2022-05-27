using Raven.Client.Documents;

namespace EmergingBooking.Infrastructure.RavenDB.Interface
{
    public interface IRavenDocumentStoreHolder
    {
        IDocumentStore Store { get; }
    }
}
