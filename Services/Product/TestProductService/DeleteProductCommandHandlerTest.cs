using BuildingBlocks.Common.Errors;
using ProductService.Api.Features.DeleteProduct;
using ProductService.Api.Features.UpdateProduct;

namespace TestProductService;

public class DeleteProductCommandHandlerTest
{
    private readonly Mock<ICommandRepository<Product>> _mockRepoCommandProduct;
    private readonly Mock<IQueryRepository<Product>> _mockRepoQueryProduct;

    public DeleteProductCommandHandlerTest()
    {
        _mockRepoCommandProduct = new Mock<ICommandRepository<Product>>();
        _mockRepoQueryProduct = new Mock<IQueryRepository<Product>>();
    }

    [Fact]
    public async Task Handle_ShouldDeleteProduct_WhenIdIsGreatherThanZero()
    {
        var expectedProduct = new Product()
        {
            Id = 1,
            CategoryId = 1,
            Description = "Test",
            Name = "Test",
            Price = 10,
        };
        _mockRepoQueryProduct.Setup(x => x.GetByIdAsync(
            It.IsAny<int>(),
            It.IsAny<Expression<Func<Product, object>>[]>()
            ))
            .ReturnsAsync(expectedProduct);

        _mockRepoCommandProduct.Setup(x => x.DeleteAsync(It.Is<int>(x => x > 0)))
            .ReturnsAsync(expectedProduct);

        var sut = new DeleteProductCommandHandler(_mockRepoCommandProduct.Object, _mockRepoQueryProduct.Object);
        var request = new DeleteProductCommandRequest(1);
        var result = await sut.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeOfType<Result<DeleteProductCommandResult>>();
    }

    [Fact]
    public async Task Handle_ShouldReturnThrowException_WhenDatabaseFails()
    {
        _mockRepoQueryProduct.Setup(x => x.GetByIdAsync(1, It.IsAny<Expression<Func<Product, object>>[]>()))
            .ReturnsAsync(new Product { Id = 1, Name = "Product 1" });

        _mockRepoCommandProduct.Setup(x => x.DeleteAsync(It.IsAny<int>()))
            .ThrowsAsync(new Exception("Database error"));

        var sut = new DeleteProductCommandHandler(_mockRepoCommandProduct.Object, _mockRepoQueryProduct.Object);

        var request = new DeleteProductCommandRequest(1);

        Func<Task> act = async () => await sut.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task Handle_ShouldReturnResultFail_WhenProductIsNotFound()
    {
        _mockRepoQueryProduct.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<Expression<Func<Product, object>>[]>()))
            .ReturnsAsync((Product)null!);

        var sut = new DeleteProductCommandHandler(_mockRepoCommandProduct.Object, _mockRepoQueryProduct.Object);

        var request = new DeleteProductCommandRequest(1);

        var result = await sut.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeOfType<Result<DeleteProductCommandResult>>();
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().BeOfType<List<IError>>();
        result.Errors.Should().ContainSingle().Which.Should().BeOfType<DetailError>();
    }
}