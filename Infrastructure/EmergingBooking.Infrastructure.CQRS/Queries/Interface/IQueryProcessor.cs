using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingBooking.Infrastructure.CQRS.Queries.Interface
{
    public interface IQueryProcessor
    {
        Task<TResult> ExecuteQueryAsync<TQueryParameter, TResult>(TQueryParameter parameter) where TQueryParameter: IQuery;
    }
}
