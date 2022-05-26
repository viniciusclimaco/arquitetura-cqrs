using EmergingBooking.Infrastructure.CQRS.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingBooking.Infrastructure.CQRS.Queries
{
    internal class QueryProcessor : IQueryProcessor
    {
        private readonly DependencyResolver _dependencyResolver;

        public QueryProcessor(DependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public async Task<TResult> ExecuteQueryAsync<TQueryParameter, TResult>(TQueryParameter queryParameter) where TQueryParameter : IQuery
        {
            if (queryParameter == null)
                throw new ArgumentNullException(nameof(queryParameter));

            var queryHandler = _dependencyResolver.Resolve<IQueryHandler<TQueryParameter, TResult>>();
            return await queryHandler.ExecuteQueryAsync(queryParameter);
        }
    }
}
