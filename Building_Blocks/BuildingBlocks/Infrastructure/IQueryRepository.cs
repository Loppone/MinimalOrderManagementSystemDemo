namespace BuildingBlocks.Infrastructure;

public interface IQueryRepository<T> where T : class
{
    public Task<PaginatedResult<T>> GetAllAsync(
         int pageNumber = 1,
         int pageSize = 10,
         params Expression<Func<T, object>>[] includes);

    public Task<T?> GetByIdAsync(
         int id,
         params Expression<Func<T, object>>[] includes);
}