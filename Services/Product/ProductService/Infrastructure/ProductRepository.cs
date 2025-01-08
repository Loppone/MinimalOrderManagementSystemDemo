using BuildingBlocks.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace ProductService.Infrastructure;

public class ProductRepository(IRepository<Product> _repository)
//: IRepository<Product>
{
    public async Task<int> AddAsync(Product entity)
    {
        return await _repository.AddAsync(entity);

        //_dbContext.Products.Add(entity);
        //await _dbContext.SaveChangesAsync();
        //return entity.Id;
    }

    public Task<Product> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbContext.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .ToListAsync();
    }

    public Task<Product?> GetByIdAsync(int id)
    {
        return _dbContext.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task UpdateAsync(Product entity)
    {
        throw new NotImplementedException();
    }
}
