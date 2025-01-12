using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductService.Features.CreateProduct;

namespace GatewayApi.ProductEndpoints
{
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
                    return Results.Problem(
                        detail: "Validation Errors",
                        statusCode: StatusCodes.Status400BadRequest,
                        extensions: new Dictionary<string, object?>
                        {
                            { "errors", result.Errors
                                .Select((e, index) => new KeyValuePair<string, object?>($"{index + 1}", e.Message))
                                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value) }
                        }
                    );
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
}
