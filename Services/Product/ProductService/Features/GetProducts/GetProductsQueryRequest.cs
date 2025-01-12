
namespace ProductService.Features.GetProducts;

public record GetProductsQueryRequest(int PageNumber = 1, int PageSize = 10) : IRequest<Result<GetProductsQueryResult>>;
