namespace ProductService.Api.Features.ImageSaved;

public class ImageSavedConsumer(
    IQueryRepository<Product> rdRepo,
    ICommandRepository<Product> rwRepo)
    : IConsumer<FileSavedEvent>
{
    public async Task Consume(ConsumeContext<FileSavedEvent> context)
    {
        var product = await rdRepo.GetByIdAsync(context.Message.EntityId);

        if (product is not null)
        {
            product.ImageId = context.Message.FileId;
        }

        await rwRepo.UpdateAsync(product!);
    }
}
