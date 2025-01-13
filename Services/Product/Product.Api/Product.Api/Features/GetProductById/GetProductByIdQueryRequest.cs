namespace ProductService.Api.Features.GetProductById;

public record GetProductByIdQueryRequest(int Id) : IRequest<Result<GetProductByIdQueryResult>>;
