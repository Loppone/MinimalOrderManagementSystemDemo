using Microsoft.AspNetCore.Http;

namespace ImageService.Api.Features.SaveImage;

public class SaveImageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/save", async ([FromBody] SaveImageDto request, IMediator mediator) =>
        {
            var imageBytes = Convert.FromBase64String(request.File);
            using var stream = new MemoryStream(imageBytes);
            
            var obj = new SaveImageCommandRequest(
                request.TypeOfEntity,
                request.EntityId,
                request.FileName,
                stream);

            var result = await mediator.Send(obj);

            if (result.IsFailed)
            {
                return ErrorHandlingHelper.HandleValidationErrors(result.Errors);
            }

            return Results.Ok(result);
        })
        .WithName("Create Product")
        .Produces<SaveImageCommandResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product"); ;
    }
}

public class SaveImageDto
{
    public int EntityId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public EntityType TypeOfEntity { get; set; }
    public string File { get; set; } = string.Empty;
}