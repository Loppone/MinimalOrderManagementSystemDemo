
namespace ProductService.Api.Features.AddImage;

public class AddImageHandler(
    IQueryRepository<Product> rdRepo,
    ICommandRepository<Product> rwRepo,
    IImageService imageService)
    : IRequestHandler<AddImageRequest, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
        AddImageRequest request,
        CancellationToken cancellationToken)
    {
        var product = await rdRepo.GetByIdAsync(request.ProductId);

        if (product is null)
            return Result.Fail(new DetailError("Product not found"));

        // Chiamo il servizio delle immagini che ritorna un Id<int> (FAKE!)
        var image = await imageService.SaveImageAsync(null!, "", "", cancellationToken);

        if (image.IsFailed)
            return Result.Fail(image.Errors);

        if (request.IsThumbnail)
            product.ImageThumbnailId = image.Value;
        else
            product.ImageId = image.Value;

        await rwRepo.UpdateAsync(product);

        return Unit.Value;
    }
}
