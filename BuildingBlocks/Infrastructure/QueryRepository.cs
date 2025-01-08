using System.Linq.Expressions;
using BuildingBlocks.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure;

public class QueryRepository<T, TContext> : IQueryRepository<T>
    where T : class
    where TContext : DbContext
{
    private readonly TContext _context;
    private readonly DbSet<T> _dbSet;

    public QueryRepository(TContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    // Ottieni tutte le entità con inclusioni e paginazione
    public async Task<PaginatedResult<T>> GetAllAsync(
        int pageNumber = 1,
        int pageSize = 10,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        // Applica gli Include dinamici
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        // Ottieni il totale degli elementi
        var totalItems = await query.CountAsync();

        // Applica la paginazione
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResult<T>
        {
            Items = items.AsQueryable(),
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
            CurrentPage = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync(entity => EF.Property<int>(entity, "Id") == id);
    }
}
