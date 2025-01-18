namespace ProductService.Api.Features.GetProducts;

public record GetProductsQueryResult(PaginatedResult<Product> Products);
