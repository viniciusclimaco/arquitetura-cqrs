using EmergingBooking.Infrastructure.SqlServer.Interface;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace EmergingBooking.Infrastructure.SqlServer
{
    internal class SqlServerStoreHolder : ISqlServerStoreHolder
    {
        private readonly SqlServerSettings _settings;
        public SqlServerStoreHolder(IOptions<SqlServerSettings> optionsDataBaseSettings)
        {
            _settings = optionsDataBaseSettings.Value;
        }

        private Lazy<IDbConnection> LazyStore => new Lazy<IDbConnection>(() => new SqlConnection(_settings.ConnectionString));

        public IDbConnection DbConnection => LazyStore.Value;

        
    }
}
