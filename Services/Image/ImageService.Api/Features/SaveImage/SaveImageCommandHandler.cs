using System.IO.Abstractions;

namespace ImageService.Api.Features.SaveImage;

public class SaveImageCommandHandler(IFileSystem fileSystem)
    : IRequestHandler<SaveImageCommandRequest, Result<SaveImageCommandResult>>
{
    public async Task<Result<SaveImageCommandResult>> Handle(SaveImageCommandRequest request, CancellationToken cancellationToken)
    {
        string filePath = fileSystem.Path.Combine(request.FilePath, $"{Guid.NewGuid()}{fileSystem.Path.GetExtension(request.FileName)}");

        if (!fileSystem.Directory.Exists(request.FilePath))
        {
            fileSystem.Directory.CreateDirectory(request.FilePath);
        }

        using var fileStream = fileSystem.File.Open(filePath, FileMode.Create, FileAccess.Write);

        await request.ImageStream.CopyToAsync(fileStream, cancellationToken);

        return Result.Ok(new SaveImageCommandResult());
    }
}
