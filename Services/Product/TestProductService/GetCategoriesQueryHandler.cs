using ProductService.Api.Features.GetCategories;

namespace TestProductService;

public class GetCategoriesQueryHandlerTest
{
    [Fact]
    public async Task Handle_ShouldReturnCategories_WhenDataFound()
    {
        var mockRepository = new Mock<IQueryRepository<Category>>();

        var expectedCategories = new List<Category>
            {
                new() { Id = 1, Name = "Category A" },
                new() { Id = 2, Name = "Category B" }
            }.AsQueryable();

        mockRepository.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Category, object>>[]>()))
            .ReturnsAsync(expectedCategories);

        var sut = new GetCategoriesQueryHandler(mockRepository.Object);

        var result = await sut.Handle(new GetCategoriesQueryRequest(), CancellationToken.None);

        result.Categories.Should().BeEquivalentTo(expectedCategories);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoDataFound()
    {
        var mockRepository = new Mock<IQueryRepository<Category>>();

        var expectedCategories = new List<Category>();

        mockRepository.Setup(x => x.GetAllAsync())
            .ReturnsAsync(expectedCategories);

        var sut = new GetCategoriesQueryHandler(mockRepository.Object);

        var result = await sut.Handle(new GetCategoriesQueryRequest(), CancellationToken.None);

        result.Categories.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_ShouldReturnException_WhenDatabaseFails()
    {
        var mockRepository = new Mock<IQueryRepository<Category>>();

        mockRepository.Setup(x => x.GetAllAsync())
            .ThrowsAsync(new Exception("Generic database error..."));

        var sut = new GetCategoriesQueryHandler(mockRepository.Object);

        Func<Task> result = () => sut.Handle(new GetCategoriesQueryRequest(), CancellationToken.None);

        await result.Should().ThrowAsync<Exception>();
    }
}