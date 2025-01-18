namespace ImageService.Api.Features.SaveImage;

public record SaveImageCommandRequest(
    EntityType Requester,
    int EntityId,
    string FileName,
    Stream ImageStream)
    : IRequest<Result<SaveImageCommandResult>>;
