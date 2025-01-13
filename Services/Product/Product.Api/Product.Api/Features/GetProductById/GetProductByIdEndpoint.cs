namespace ProductService.Api.EndpointsProductEndpoints;

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/product/{id}", async (int id, IMediator mediator) =>
        {
            var result = await mediator.Send(
                new GetProductByIdQueryRequest(id));

            if (result.IsFailed)
            {
                return ErrorHandlingHelper.HandleValidationErrors(result.Errors);
            }

            return Results.Ok(result.Value.Product);
        })
        .WithName("GetProductById")
        .Produces<GetProductByIdQueryResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
    }
}
