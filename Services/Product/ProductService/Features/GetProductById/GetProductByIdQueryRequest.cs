namespace ProductService.Features.GetProductById;

public record GetProductByIdQueryRequest(int Id) : IRequest<Result<GetProductByIdQueryResult>>;
