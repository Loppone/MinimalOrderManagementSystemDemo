
namespace ProductService.Api.Features.AddImage;

public class FakeImageService : IImageService
{
    public Task<Result> DeleteImageAsync(int imageId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Stream> GetImageAsync(int imageId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<int>> SaveImageAsync(Stream imageStream, string fileName, string imageFormat, CancellationToken cancellationToken)
    {
        int randomId = new Random().Next(0, 100000);
        return await Task.FromResult(randomId);
    }
}
