namespace TestProductService;

public class UpdateProductQueryHandlerTest
{
    private readonly Mock<ICommandRepository<Product>> _mockRepoCommandProduct;
    private readonly Mock<IQueryRepository<Product>> _mockRepoQueryProduct;

    public UpdateProductQueryHandlerTest()
    {
        _mockRepoCommandProduct = new Mock<ICommandRepository<Product>>();
        _mockRepoQueryProduct = new Mock<IQueryRepository<Product>>();
    }


    [Fact]
    public async Task Handle_ShouldReturnModifiedProduct_WhenDataIsValid()
    {
        var foundProduct = new Product()
        {
            Id = 1,
            CategoryId = 1,
            Description = "Test",
            Name = "Test",
            Price = 10,
        };

        var expectedProduct = new Product()
        {
            Id = 1,
            CategoryId = 1,
            Description = "Test mod",
            Name = "Test mod",
            Price = 100,
        };


        _mockRepoQueryProduct.Setup(x => x.GetByIdAsync(1, It.IsAny<Expression<Func<Product, object>>[]>()))
            .ReturnsAsync(foundProduct);

        _mockRepoCommandProduct.Setup(x=> x.UpdateAsync(It.IsAny<Product>()))
            .ReturnsAsync(expectedProduct);


        var sut = new UpdateProductCommandHandler(_mockRepoCommandProduct.Object, _mockRepoQueryProduct.Object);

        var request = new UpdateProductCommandRequest(expectedProduct.Id,expectedProduct.Name,expectedProduct.Description,expectedProduct.Price);

        var result = await sut.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeOfType<Result<UpdateProductCommandResult>>();
        result.Value.Should().NotBeNull();
        result.Value.Product.Should().BeEquivalentTo(expectedProduct);
    }

    [Fact]
    public async Task Handle_ShouldReturnResultFail_WhenProductNotFound()
    {
        _mockRepoQueryProduct.Setup(x => x.GetByIdAsync(1, It.IsAny<Expression<Func<Product, object>>[]>()))
            .ReturnsAsync((Product)null!);

        var sut = new UpdateProductCommandHandler(_mockRepoCommandProduct.Object, _mockRepoQueryProduct.Object);
   
        var request = new UpdateProductCommandRequest(1, "Product 1", "Product 1", 100);
        
        var result = await sut.Handle(request, CancellationToken.None);
        
        result.Should().NotBeNull();
        result.Should().BeOfType<Result<UpdateProductCommandResult>>();
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().BeOfType<List<IError>>();
        result.Errors.Should().ContainSingle().Which.Should().BeOfType<DetailError>();
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenDatabaseFails()
    {
        _mockRepoQueryProduct.Setup(x => x.GetByIdAsync(1, It.IsAny<Expression<Func<Product, object>>[]>()))
            .ReturnsAsync(new Product { Id = 1, Name = "Product 1" });

        _mockRepoCommandProduct.Setup(x => x.UpdateAsync(It.IsAny<Product>()))
            .ThrowsAsync(new Exception("Database error"));

        var sut = new UpdateProductCommandHandler(_mockRepoCommandProduct.Object, _mockRepoQueryProduct.Object);
      
        var request = new UpdateProductCommandRequest(1, "Product 1", "Description", 100);
      
        Func<Task> act = async () => await sut.Handle(request, CancellationToken.None);
      
        await act.Should().ThrowAsync<Exception>();
    }
}