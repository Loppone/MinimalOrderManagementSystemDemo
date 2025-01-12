using BuildingBlocks.Common.Errors;

namespace GatewayApi.ProductEndpoints;

public class CreateProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/product", async ([FromBody] CreateProductCommandRequest request, IMediator mediator) =>
        {
            var result = await mediator.Send(
                new CreateProductCommandRequest(
                    request.CategoryId,
                    request.Name,
                    request.Description,
                    request.Price));

            if (result.IsFailed)
            {
                return ErrorHandlingHelper.HandleValidationErrors(result.Errors);
            }

            return Results.Ok(result.Value.Id);
        })
        .WithName("Create Product")
        .Produces<CreateProductCommandResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
    }
}
