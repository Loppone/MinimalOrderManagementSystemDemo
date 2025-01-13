[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("TestProductService")]

namespace ProductService.Api.Features.GetProducts;

internal class GetProductsQueryHandler(IQueryRepository<Product> repository)
    : MediatR.IRequestHandler<GetProductsQueryRequest, Result<GetProductsQueryResult>>
{
    public async Task<Result<GetProductsQueryResult>> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var paginatedProducts = await repository.GetAllAsync(request.PageNumber, request.PageSize, x => x.Category!);
        return new GetProductsQueryResult(paginatedProducts.Items);
    }
}
