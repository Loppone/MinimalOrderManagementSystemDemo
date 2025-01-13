namespace ProductService.Api.Features.CreateProduct;

internal sealed class CreateProductCommandHandler(
    ICommandRepository<Product> _repository,
    IQueryRepository<Category> _repositoryCategory
    )
    : IRequestHandler<CreateProductCommandRequest, Result<CreateProductCommandResult>>
{
    public async Task<Result<CreateProductCommandResult>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var cat = await _repositoryCategory.GetByIdAsync(request.CategoryId);

        if (cat is null)
        {
            return Result.Fail(new DetailError("Category not found"));
        }

        var product = request.Adapt<Product>();

        await _repository.AddAsync(product);

        if (product.Id <= 0)
        {
            return Result.Fail(new DetailError("Insert Product error"));
        }

        await _repository.SaveAsync();

        return new CreateProductCommandResult(product.Id);
    }
}
