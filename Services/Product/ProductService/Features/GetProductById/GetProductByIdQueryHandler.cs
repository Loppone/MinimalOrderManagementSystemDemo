[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("TestProductService")]

namespace ProductService.Features.GetProductById;

public record GetProductByIdQuery(int Id) : IRequest<GetProductByIdResult>;
public record GetProductByIdResult(Product? Product);

internal class GetProductByIdQueryHandler(IQueryRepository<Product> _repository) :
    IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, x => x.Category!);

        return product is null ?
            throw new NotFoundException($"Product with id {request.Id} not found.") :
            new GetProductByIdResult(product);
    }
}
