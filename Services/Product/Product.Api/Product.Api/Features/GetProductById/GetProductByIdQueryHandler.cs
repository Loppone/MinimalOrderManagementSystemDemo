[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("TestProductService")]

namespace ProductService.Api.Features.GetProductById;

internal sealed class GetProductByIdQueryHandler(IQueryRepository<Product> _repository) 
    : IRequestHandler<GetProductByIdQueryRequest, Result<GetProductByIdQueryResult>>
{
    public async Task<Result<GetProductByIdQueryResult>> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, x => x.Category!);

        return product is null ?
            Result.Fail(new DetailError($"Product with id {request.Id} not found.")) :
            new GetProductByIdQueryResult(product);
    }
}
