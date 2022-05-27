using EmergingBooking.Infrastructure.SqlServer.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmergingBooking.Infrastructure.SqlServer
{
    public static class RegisterStoreageSqlServerInfra
    {
        public static IServiceCollection RegisterSqlServerInfraDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SqlServerSettings>(setting =>
            {
                configuration.GetSection(nameof(SqlServerSettings)).Bind(setting);
            });

            services.AddTransient<ISqlServerStoreHolder, SqlServerStoreHolder>();

            return services;
        }
    }
}
