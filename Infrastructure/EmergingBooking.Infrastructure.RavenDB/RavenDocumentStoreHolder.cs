using EmergingBooking.Infrastructure.RavenDB.Interface;
using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using Raven.Client.Json.Serialization.NewtonsoftJson;
using Raven.Client.ServerWide.Operations;

namespace EmergingBooking.Infrastructure.RavenDB
{
    internal class RavenDocumentStoreHolder : IRavenDocumentStoreHolder
    {
        private readonly RavendDbSettings _settings;

        public RavenDocumentStoreHolder(IOptions<RavendDbSettings> optionsDatabaseSettings)            
        {
            _settings = optionsDatabaseSettings.Value;
        }

        private Lazy<IDocumentStore> LazyStore => new Lazy<IDocumentStore>(() =>
        {
            var store = new DocumentStore
            {
                Urls = new[] { _settings.Server },
                Database = _settings.DatabaseName,
                Conventions =
                {
                    Serialization = new NewtonsoftJsonSerializationConventions
                    {
                        CustomizeJsonSerializer = serializer =>
                        {
                            serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
                        }
                    }
                }
            };

            store.Initialize();

            var databaseRecord = store.Maintenance.Server.Send(new GetDatabaseRecordOperation(store.Database));
            if (databaseRecord != null)
                return store;

            var createDataBaseOperation = new CreateDatabaseOperation(new Raven.Client.ServerWide.DatabaseRecord(store.Database));
            store.Maintenance.Server.Send(createDataBaseOperation);

            return store;
        });

        public IDocumentStore Store => LazyStore.Value;

    }
}
