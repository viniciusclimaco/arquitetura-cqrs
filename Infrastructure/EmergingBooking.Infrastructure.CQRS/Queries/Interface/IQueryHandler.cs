namespace EmergingBooking.Infrastructure.CQRS.Queries.Interface
{
    public interface IQueryHandler<in TQueryParameter, TResult> where TQueryParameter : IQuery
    {
        Task<TResult> ExecuteQueryAsync(TQueryParameter parameter);
    }
}
