namespace ProductService.Api.Features.DeleteProduct;

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/product/{id}", async (int id, IMediator mediator) =>
        {
            var result = await mediator.Send(
                new DeleteProductCommandRequest(id));

            if (result.IsFailed)
            {
                return ErrorHandlingHelper.HandleValidationErrors(result.Errors);
            }

            return Results.Ok(result.Value.Product);
        })
        .WithName("Delete Product")
        .Produces<DeleteProductCommandResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Product")
        .WithDescription("Delete Product");
    }
}
