namespace TestProduct;

public class InMemoryDb<T> where T : DbContext
{
    public T DbContext { get; set; }

    public InMemoryDb(Func<DbContextOptions<T>, T> dbContextFactory)
    {
        var options = new DbContextOptionsBuilder<T>()
            .UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}")
            .Options;

        DbContext = dbContextFactory(options);
    }
}
