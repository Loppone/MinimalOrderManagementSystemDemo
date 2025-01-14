using ProductService.Api.Features.UpdateProduct;

namespace ProductService.Api.EndpointsProductEndpoints;

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/product/{id}", async (int id, [FromBody] UpdateProductCommandRequest request, IMediator mediator) =>
        {
            var result = await mediator.Send(
                new UpdateProductCommandRequest(
                    id,
                    request.Name,
                    request.Description,
                    request.Price));

            if (result.IsFailed)
            {
                return ErrorHandlingHelper.HandleValidationErrors(result.Errors);
            }

            return Results.Ok(result.Value);
        })
        .WithName("Update Product")
        .Produces<UpdateProductCommandResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
    }
}
