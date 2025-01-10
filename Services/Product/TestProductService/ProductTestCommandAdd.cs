
namespace TestProductService;

public class ProductTestCommandAdd
{
    private readonly Mock<ICommandRepository<Product>> _mockRepository;

    public ProductTestCommandAdd()
    {
        _mockRepository = new Mock<ICommandRepository<Product>>();
    }


    [Fact]
    public async Task Handle_ShouldAddProduct()
    {
        _mockRepository.Setup(x => x.AddAsync(It.IsAny<Product>()))
            .Callback<Product>(x => x.Id = 1);

        var sut = new CreateProductCommandHandler(_mockRepository.Object);

        var request = new CreateProductCommandRequest(1, "Product 1", "Product 1", 100);

        var result = await sut.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeOfType<CreateProductCommandResult>();
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenDatabaseFails()
    {
        _mockRepository.Setup(x => x.AddAsync(It.IsAny<Product>()))
            .ThrowsAsync(new Exception("Database error"));

        var sut = new CreateProductCommandHandler(_mockRepository.Object);

        var request = new CreateProductCommandRequest(1, "Product 1", "Description", 100);

        Func<Task> act = async () => await sut.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Handle_ShouldThrowException_WhenAddAsyncReturnsZero(int prodcutId)
    {
        _mockRepository.Setup(x => x.AddAsync(It.IsAny<Product>()))
            .Callback<Product>(x => x.Id = prodcutId);

        var sut = new CreateProductCommandHandler(_mockRepository.Object);

        var request = new CreateProductCommandRequest(1, "Product 1", "Description", 100);

        Func<Task> act = async () => await sut.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(1)]
    public async Task Handle_ShouldThrowException_WhenCategoryILessOrEqualToZero(int categoryId)
    {
        _mockRepository.Setup(x => x.AddAsync(It.IsAny<Product>()))
            .Callback<Product>(x => x.CategoryId = categoryId);

        var sut = new CreateProductCommandHandler(_mockRepository.Object);
        var request = new CreateProductCommandRequest(categoryId, "Product 1", "Description", 100);

        Func<Task> act = async () => await sut.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>();
    }
}
