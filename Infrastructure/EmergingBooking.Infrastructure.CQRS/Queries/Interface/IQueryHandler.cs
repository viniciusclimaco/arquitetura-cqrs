using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingBooking.Infrastructure.CQRS.Queries.Interface
{
    public interface IQueryHandler<in TQueryParameter, TResult> where TQueryParameter : IQuery
    {
        Task<TResult> ExecuteQueryAsync(TQueryParameter parameter);
    }
}
