namespace TestProductService
{
    public class ProductTestQueryGetById
    {
        [Fact]
        public async Task Handle_ReturnProduct_WhenNoDataFound()
        {
            var mockRepository = new Mock<IQueryRepository<Product>>();

            mockRepository
                .Setup(x => x.GetByIdAsync(1, It.IsAny<Expression<Func<Product, object>>[]>()))
                .ReturnsAsync((Product)null!);

            var sut = new GetProductByIdQueryHandler(mockRepository.Object);

            Func<Task> act = async () => await sut.Handle(new GetProductByIdQuery(1), CancellationToken.None);

            await act.Should().ThrowAsync<NotFoundException>();
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

            var result = await sut.Handle(new GetProductByIdQuery(1), CancellationToken.None);

            result.Product.Should().BeEquivalentTo(expectedProduct);
        }
    }
}