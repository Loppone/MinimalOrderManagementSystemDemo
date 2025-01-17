namespace TestProductService;

public class AddImageHandlerTest
{
    private readonly Mock<ICommandRepository<Product>> _mockCommandRepository;
    private readonly Mock<IQueryRepository<Product>> _mockQueryRepository;
    private readonly Mock<IImageService> _mockImageService;
    private readonly AddImageHandler _handler;
   
    public AddImageHandlerTest()
    {
        _mockCommandRepository = new Mock<ICommandRepository<Product>>();
        _mockQueryRepository = new Mock<IQueryRepository<Product>>();
        _mockImageService = new Mock<IImageService>();

        _handler = new AddImageHandler(
            _mockQueryRepository.Object,
            _mockCommandRepository.Object,
            _mockImageService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnResultError_WhenProductIsNotFound()
    {

    }

    [Fact]
    public async Task Handle_ShoudlUpdateImage_WhenServiceUptadeWithSuccess()
    {

    }

    [Fact]
    public async Task Handle_ShoudlUpdateThumbnail_WhenServiceUptadeWithSuccess()
    {

    }

    [Fact]
    public async Task Handle_ShouldReturnResultFail_WhenImageServiceFails()
    {

    }

}