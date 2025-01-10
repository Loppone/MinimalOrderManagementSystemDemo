using BuildingBlocks.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure;

public class CommandRepository<T, TContext> : ICommandRepository<T>
    where T : class, IEntity
    where TContext : DbContext
{
    private readonly TContext _context;
    private readonly DbSet<T> _dbSet;

    public CommandRepository(TContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<int> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id; 
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);

        if (entity == null)
        {
            return null!;
        }
     
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}

