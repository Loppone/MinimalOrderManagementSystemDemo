namespace BuildingBlocks.Infrastructure;

public interface ICommandRepository<T> where T : class 
{
    Task<int> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(int id);
}
