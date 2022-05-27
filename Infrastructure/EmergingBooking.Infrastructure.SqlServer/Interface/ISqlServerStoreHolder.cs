using System.Data;

namespace EmergingBooking.Infrastructure.SqlServer.Interface
{
    public interface ISqlServerStoreHolder
    {
        IDbConnection DbConnection { get; }

    }
}
