namespace ProductService.Api.EndpointsProductEndpoints;

public class CreateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/product/category", async ([FromBody] CreateCategoryCommandRequest request, IMediator mediator) =>
        {
            var result = await mediator.Send(
                new CreateCategoryCommandRequest(request.Name));

            if (result.IsFailed)
            {
                return ErrorHandlingHelper.HandleValidationErrors(result.Errors);
            }

            return Results.Ok(result.Value.Id);
        })
        .WithName("Create Category")
        .Produces<CreateCategoryCommandResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Category")
        .WithDescription("Create Cateogry");
    }
}
