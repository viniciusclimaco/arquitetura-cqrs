using EmergingBooking.Infrastructure.RavenDB.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using Raven.Client.Json.Serialization.NewtonsoftJson;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace EmergingBooking.Infrastructure.RavenDB
{
    public static class RegisterStoreRavenDBInfrastructure
    {
        public static IServiceCollection RegisterRavenDbStoreStorageInfraDependencies(this IServiceCollection services, IConfiguration configuration)
        {   
            services.Configure<RavendDbSettings>(setting =>
            {
                configuration.GetSection(nameof(RavendDbSettings)).Bind(setting);
            });

            services.AddTransient<IRavenDocumentStoreHolder, RavenDocumentStoreHolder>();

            return services;
        }
    }
}
