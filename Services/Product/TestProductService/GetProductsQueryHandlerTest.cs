namespace TestProductService;

public class GetProductsQueryHandlerTest
{
    [Fact]
    public async Task Handle_GetAllShouldBeCalledJustOnce()
    {
        //Arrange
        var mockRepository = new Mock<IQueryRepository<Product>>();

        mockRepository.Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Expression<Func<Product, object>>[]>()))
            .ReturnsAsync(new PaginatedResult<Product> { Items = new List<Product>().AsQueryable() });

        var sut = new GetProductsQueryHandler(mockRepository.Object);

        //Act
        await sut.Handle(new GetProductsQueryRequest(), CancellationToken.None);

        //Assert
        mockRepository.Verify(x => x.GetAllAsync(1, 10, x => x.Category!), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnException_WhenDatabaseFails()
    {
        var mockRepository = new Mock<IQueryRepository<Product>>();

        mockRepository.Setup(x => x.GetAllAsync(1, 10, x => x.Category!))
            .ThrowsAsync(new Exception("Generic database error..."));

        var sut = new GetProductsQueryHandler(mockRepository.Object);

        Func<Task> act = async () => await sut.Handle(new GetProductsQueryRequest(), CancellationToken.None);

        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task Handle_ShouldReturnProducts_WhenDataFound()
    {
        var mockRepository = new Mock<IQueryRepository<Product>>();

        var expectedProducts = new List<Product>
            {
                new() { Id = 1, Name = "Product A", Price = 100, Category = new Category() },
                new() { Id = 2, Name = "Product B", Price = 200, Category = new Category() }
            };

        var paginatedResult = new PaginatedResult<Product>
        {
            Items = expectedProducts.AsQueryable(),
            TotalItems = 2,
            TotalPages = 1,
            CurrentPage = 1,
            PageSize = 10
        };

        mockRepository.Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Expression<Func<Product, object>>[]>()))
            .ReturnsAsync(paginatedResult);

        var sut = new GetProductsQueryHandler(mockRepository.Object);

        var result = await sut.Handle(new GetProductsQueryRequest(), CancellationToken.None);

        result.Value.Products.Should().BeEquivalentTo(expectedProducts);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoDataFound()
    {
        var mockRepository = new Mock<IQueryRepository<Product>>();

        var expectedProducts = new List<Product>();

        var paginatedResult = new PaginatedResult<Product>
        {
            Items = expectedProducts.AsQueryable(),
            TotalItems = 0,
            TotalPages = 1,
            CurrentPage = 1,
            PageSize = 10
        };

        mockRepository.Setup(x => x.GetAllAsync(1, 10, x => x.Category!))
            .ReturnsAsync(paginatedResult);

        var sut = new GetProductsQueryHandler(mockRepository.Object);

        var result = await sut.Handle(new GetProductsQueryRequest(), CancellationToken.None);

        result.Value.Products.Should().BeEmpty();
    }
}