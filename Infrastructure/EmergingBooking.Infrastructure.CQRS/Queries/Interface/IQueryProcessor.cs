namespace EmergingBooking.Infrastructure.CQRS.Queries.Interface
{
    public interface IQueryProcessor
    {
        Task<TResult> ExecuteQueryAsync<TQueryParameter, TResult>(TQueryParameter parameter) where TQueryParameter: IQuery;
    }
}
