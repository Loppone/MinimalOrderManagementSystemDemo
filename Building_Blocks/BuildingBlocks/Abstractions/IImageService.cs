namespace BuildingBlocks.Abstractions;

public interface IImageService
{
    Task<Stream> GetImageAsync(int imageId, CancellationToken cancellationToken);
    Task<Result<int>> SaveImageAsync(Stream imageStream, string fileName, string imageFormat, CancellationToken cancellationToken);
    Task<Result> DeleteImageAsync(int imageId, CancellationToken cancellationToken);

}