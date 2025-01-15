namespace ProductService.Api.Features.DeleteProduct;

public record DeleteProductCommandRequest(int Id)
    : IRequest<Result<DeleteProductCommandResult>>;
