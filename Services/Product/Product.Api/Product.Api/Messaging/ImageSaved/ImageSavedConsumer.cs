namespace ProductService.Api.Messaging.ImageSaved;

public class ImageSavedConsumer : IConsumer<FileSavedEvent>
{
    private readonly IQueryRepository<Product> _rdRepo;
    private readonly ICommandRepository<Product> _rwRepo;

    public ImageSavedConsumer(
        IQueryRepository<Product> rdRepo,
        ICommandRepository<Product> rwRepo)
    {
        _rdRepo = rdRepo ?? throw new ArgumentNullException(nameof(rdRepo));
        _rwRepo = rwRepo ?? throw new ArgumentNullException(nameof(rwRepo));
    }

    public async Task Consume(ConsumeContext<FileSavedEvent> context)
    {
        if (context.Message.TypeOfEntity != BuildingBlocks.Messaging.Enums.EntityType.Product)
            return;

        var product = await _rdRepo.GetByIdAsync(context.Message.EntityId);

        if (product is not null)
        {
            product.ImageId = context.Message.FileId;
            await _rwRepo.UpdateAsync(product);
        }
    }
}
