namespace ImageService.Api.Features.SaveImage;

public class SaveImageCommandHandler(
    ICommandRepository<Image> rwRepo,
    IFileSystem fileSystem,
    IOptions<ImageConfiguration> storageOptions,
    IPublishEndpoint publishEndpoint)
    : IRequestHandler<SaveImageCommandRequest, Result<SaveImageCommandResult>>
{
    public async Task<Result<SaveImageCommandResult>> Handle(SaveImageCommandRequest request, CancellationToken cancellationToken)
    {
        var imagePath = storageOptions.Value.PathFolder;

        if (imagePath is null)
            return Result.Fail(new DetailError("Image path is missing in configuration"));

        string filePath = fileSystem.Path.Combine(imagePath, $"{Guid.NewGuid()}{fileSystem.Path.GetExtension(request.FileName)}");

        if (!fileSystem.Directory.Exists(imagePath))
        {
            fileSystem.Directory.CreateDirectory(imagePath);
        }

        using var fileStream = fileSystem.File.Open(filePath, FileMode.Create, FileAccess.Write);
        await request.ImageStream.CopyToAsync(fileStream, cancellationToken);

        // Salva il file su DB
        var fileDb = new ImageService.Api.Domain.Models.Image
        {
            FileName=request.FileName,
            Path = imagePath,
            TypeOfEntity = request.Requester
        };

        var fileId = await rwRepo.AddAsync(fileDb);

        await rwRepo.SaveAsync();

        // Messaggio da dispatchare
        await publishEndpoint.Publish(new FileSavedEvent(
            fileId,
            request.EntityId,
            request.Requester,
            DateTime.UtcNow
            ),
            cancellationToken);

        return Result.Ok(new SaveImageCommandResult());
    }
}
