using FluentResults;
using MediatR;

namespace ImageService.Api.Features.SaveImage;

public record SaveImageCommandRequest(
    EntityType Requester,
    string FileName,
    string FilePath,
    Stream ImageStream)
    : IRequest<Result<SaveImageCommandResult>>;
