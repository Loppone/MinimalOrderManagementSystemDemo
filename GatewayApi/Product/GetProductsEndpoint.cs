﻿namespace GatewayApi.ProductEndpoints;

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (int? pageNumber, int? pageSize, IMediator mediator) =>
        {
            var result = await mediator.Send(
                new GetProductsQueryRequest(
                    pageNumber.HasValue ? pageNumber!.Value : 1,
                    pageSize.HasValue ? pageSize!.Value : 10));

            return Results.Ok(result.Products);
        })
        .WithName("GetProducts")
        .Produces<GetProductsQueryResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}
