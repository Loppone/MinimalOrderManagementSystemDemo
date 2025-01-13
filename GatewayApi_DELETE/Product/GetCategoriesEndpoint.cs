namespace GatewayApi.ProductEndpoints;

public class GetCategoriesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/product/categories", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetCategoriesQueryRequest());

            return Results.Ok(result.Categories);
        })
        .WithName("GetCategories")
        .Produces<GetCategoriesQueryResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Categories")
        .WithDescription("Get Categories");
    }
}
