using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestProduct;

public class InMemoryDb<T> where T : DbContext
    //: IAsyncLifetime
{
    public T DbContext { get; set; }

    public InMemoryDb(Func<DbContextOptions<T>, T> dbContextFactory)
    {
        var options = new DbContextOptionsBuilder<T>()
            .UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}")
            .Options;

        DbContext = dbContextFactory(options);
    }

    //public Task InitializeAsync()
    //{
    //    //DbContext.Database.EnsureCreated();
    //    return Task.CompletedTask;
    //}

    //public Task DisposeAsync()
    //{
    //    DbContext.Database.EnsureDeleted();
    //    DbContext.Dispose();
    //    return Task.CompletedTask;
    //}
}
