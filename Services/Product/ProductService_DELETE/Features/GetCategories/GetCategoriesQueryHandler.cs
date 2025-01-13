
using ProductService.Features.GetProducts;

namespace ProductService.Features.GetCategories;

internal sealed class GetCategoriesQueryHandler(IQueryRepository<Category> repository)
    : IRequestHandler<GetCategoriesQueryRequest, GetCategoriesQueryResult>
{
    public async Task<GetCategoriesQueryResult> Handle(GetCategoriesQueryRequest request, CancellationToken cancellationToken)
    {
        var categories = await repository.GetAllAsync();
        return new GetCategoriesQueryResult(categories);
    }
}