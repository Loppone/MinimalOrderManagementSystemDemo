    [assembly: System.Runtime.CompilerServices.InternalsVisibleTo("TestProductService")]

    namespace ProductService.Features.GetProducts;

    public record GetProductsQuery(int PageNumber = 1, int PageSize = 10) : IRequest<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);

    internal class GetProductsQueryHandler(IQueryRepository<Product> repository)
        : MediatR.IRequestHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var paginatedProducts = await repository.GetAllAsync(request.PageNumber, request.PageSize, x => x.Category!);
            return new GetProductsResult(paginatedProducts.Items);
        }
    }
