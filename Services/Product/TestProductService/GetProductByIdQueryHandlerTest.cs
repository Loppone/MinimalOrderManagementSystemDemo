namespace TestProductService;

public class GetProductByIdQueryHandlerTest
{
    [Fact]
    public async Task Handle_ReturnProduct_WhenNoDataFound()
    {
        var mockRepository = new Mock<IQueryRepository<Product>>();

        mockRepository
            .Setup(x => x.GetByIdAsync(1, It.IsAny<Expression<Func<Product, object>>[]>()))
            .ReturnsAsync((Product)null!);

        var sut = new GetProductByIdQueryHandler(mockRepository.Object);

        var result = await sut.Handle(new GetProductByIdQueryRequest(1), CancellationToken.None);

        result.Should().BeOfType<Result<GetProductByIdQueryResult>>();
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_ReturnProduct_WhenDataFound()
    {
        var mockRepository = new Mock<IQueryRepository<Product>>();
        var expectedProduct = new Product { Id = 1, Name = "Product A", Price = 100, Category = new Category() };

        mockRepository
            .Setup(x => x.GetByIdAsync(1, It.IsAny<Expression<Func<Product, object>>[]>()))
            .ReturnsAsync(expectedProduct);

        var sut = new GetProductByIdQueryHandler(mockRepository.Object);

        var result = await sut.Handle(new GetProductByIdQueryRequest(1), CancellationToken.None);

        result.Value.Product.Should().BeEquivalentTo(expectedProduct);
    }
}