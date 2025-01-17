
namespace ProductService.Api.Features.AddImage;

public record AddImageRequest(int ProductId, bool IsThumbnail) : IRequest<Result<Unit>>
{
}