namespace ProductService.Api.Features.CreateProduct;

public record CreateProductCommandRequest(int CategoryId, string Name, string Description, decimal Price)
    : IRequest<Result<CreateProductCommandResult>>;
