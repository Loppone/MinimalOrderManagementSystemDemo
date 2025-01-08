using BuildingBlocks.Infrastructure;
using FluentAssertions;
using Moq;
using ProductService.Infrastructure;
using ProductService.Models;
using TestProduct;

namespace TestProductService;

public class RepositoryTest
{
    [Fact]
    public async Task GetAll_ThrowException_WhenDatabaseFails()
    {
        var context = new ProductDbContext();
        var mockContext = new Mock<ProductDbContext>();
        mockContext.Setup(x => x.Products).Throws(new Exception("Generic database error..."));

        var repository = new QueryRepository<Product, ProductDbContext>(mockContext.Object);

        Func<Task> act = async () => await repository.GetAllAsync();
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfProducts_WhenDataFound()
    {
        var context = new InMemoryDb<ProductDbContext>(opt => new ProductDbContext(opt));

        await context.DbContext.Products.AddRangeAsync(new List<Product>
        {
            new() { Id = 1, Name = "Product A", Price = 100, Category = new Category() },
            new() { Id = 2, Name = "Product B", Price = 200, Category = new Category() }
        });

        await context.DbContext.SaveChangesAsync();

        var repository = new QueryRepository<Product, ProductDbContext>(context.DbContext);

        var result = await repository.GetAllAsync(1, 10, x => x.Category!);

        result.Items.Should().NotBeNull();
        result.Items.Should().NotBeNull();
        result.Items.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoDataFound()
    {
        var context = new InMemoryDb<ProductDbContext>(opt => new ProductDbContext(opt)); // Fresh new db!
        var repository = new QueryRepository<Product, ProductDbContext>(context.DbContext);

        var result = await repository.GetAllAsync(1, 10, x => x.Category!);

        result.Items.Should().NotBeNull();
        result.Items.Should().NotBeNull();
        result.Items.Should().HaveCount(0);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProduct_WhenDataFound()
    {
        var context = new InMemoryDb<ProductDbContext>(opt => new ProductDbContext(opt));

        await context.DbContext.Products.AddRangeAsync(new List<Product>
        {
            new() { Id = 1, Name = "Product A", Price = 100, Category = new Category() },
            new() { Id = 2, Name = "Product B", Price = 200, Category = new Category() }
        });

        await context.DbContext.SaveChangesAsync();

        var repository = new QueryRepository<Product, ProductDbContext>(context.DbContext);

        var result = await repository.GetByIdAsync(1);

        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNoDataFound()
    {
        var context = new InMemoryDb<ProductDbContext>(opt => new ProductDbContext(opt));
        var repository = new QueryRepository<Product, ProductDbContext>(context.DbContext);

        var result = await repository.GetByIdAsync(1);

        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_ThrowException_WhenDatabaseFails()
    {
        var context = new ProductDbContext();
        var mockContext = new Mock<ProductDbContext>();
        mockContext.Setup(x => x.Products).Throws(new Exception("Generic database error..."));
        var repository = new QueryRepository<Product, ProductDbContext>(mockContext.Object);

        Func<Task> act = async () => await repository.GetByIdAsync(1);

        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task AddAsync_ShouldAddProduct_WhenDataIsValid()
    {
        var context = new InMemoryDb<ProductDbContext>(opt => new ProductDbContext(opt));
        var repositoryCommand = new CommandRepository<Product, ProductDbContext>(context.DbContext);
        var repositoryQuery = new QueryRepository<Product, ProductDbContext>(context.DbContext);

        var product = new Product
        {
            Id = 1,
            Name = "Product A",
            Price = 100,
            Category = new Category()
        };

        await repositoryCommand.AddAsync(product);

        var result = await repositoryQuery.GetByIdAsync(1);

        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
    }
}
