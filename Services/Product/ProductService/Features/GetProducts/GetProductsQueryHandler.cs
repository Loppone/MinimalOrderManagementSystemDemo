[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("TestProductService")]

namespace ProductService.Features.GetProducts;

internal class GetProductsQueryHandler(IQueryRepository<Product> repository)
    : MediatR.IRequestHandler<GetProductsQueryRequest, GetProductsQueryResult>
{
    public async Task<GetProductsQueryResult> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var paginatedProducts = await repository.GetAllAsync(request.PageNumber, request.PageSize, x => x.Category!);
        return new GetProductsQueryResult(paginatedProducts.Items);
    }
}
