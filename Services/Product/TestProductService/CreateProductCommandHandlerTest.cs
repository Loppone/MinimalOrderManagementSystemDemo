namespace TestProductService;

public class CreateProductCommandHandlerTest
{
    private readonly Mock<ICommandRepository<Product>> _mockRepoProduct;
    private readonly Mock<IQueryRepository<Category>> _mockRepoCategory;

    public CreateProductCommandHandlerTest()
    {
        _mockRepoProduct = new Mock<ICommandRepository<Product>>();
        _mockRepoCategory = new Mock<IQueryRepository<Category>>();
    }


    [Fact]
    public async Task Handle_ShouldAddProduct()
    {
        _mockRepoCategory.Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(new Category { Id = 1, Name = "Category 1" });

        _mockRepoProduct.Setup(x => x.AddAsync(It.IsAny<Product>()))
            .Callback<Product>(x => x.Id = 1);

        var sut = new CreateProductCommandHandler(_mockRepoProduct.Object, _mockRepoCategory.Object);

        var request = new CreateProductCommandRequest(1, "Product 1", "Product 1", 100);

        var result = await sut.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeOfType<Result<CreateProductCommandResult>>();
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenDatabaseFails()
    {
        _mockRepoCategory.Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(new Category { Id = 1, Name = "Category 1" });

        _mockRepoProduct.Setup(x => x.AddAsync(It.IsAny<Product>()))
            .ThrowsAsync(new Exception("Database error"));

        var sut = new CreateProductCommandHandler(_mockRepoProduct.Object, _mockRepoCategory.Object);

        var request = new CreateProductCommandRequest(1, "Product 1", "Description", 100);

        Func<Task> act = async () => await sut.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Handle_ShouldReturnResultFail_WhenAddAsyncReturnsZero(int prodcutId)
    {
        _mockRepoCategory.Setup(x => x.GetByIdAsync(1))
    .       ReturnsAsync(new Category { Id = 1, Name = "Category 1" });

        _mockRepoProduct.Setup(x => x.AddAsync(It.IsAny<Product>()))
            .Callback<Product>(x => x.Id = prodcutId);

        var sut = new CreateProductCommandHandler(_mockRepoProduct.Object, _mockRepoCategory.Object);

        var request = new CreateProductCommandRequest(1, "Product 1", "Description", 100);

        var result = await sut.Handle(request, CancellationToken.None);

        result.Should().BeOfType<Result<CreateProductCommandResult>>();
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_ShouldReturnResultFail_WhenCategoryIsNotFound()
    {
        _mockRepoCategory.Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync((Category)null!);

        _mockRepoProduct.Setup(x => x.AddAsync(It.IsAny<Product>()))
            .Callback<Product>(x => x.Id = 1);

        var sut = new CreateProductCommandHandler(_mockRepoProduct.Object, _mockRepoCategory.Object);

        var request = new CreateProductCommandRequest(1, "Product 1", "Description", 100);
        
        var result = await sut.Handle(request, CancellationToken.None);
        
        result.Should().BeOfType<Result<CreateProductCommandResult>>();
        result.IsSuccess.Should().BeFalse();
    }
}
