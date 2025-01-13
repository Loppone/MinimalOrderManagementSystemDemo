namespace ProductService.Features.CreateCategory;

internal sealed class CreateCategoryCommandHandler(ICommandRepository<Category> _repository)
    : IRequestHandler<CreateCategoryCommandRequest, Result<CreateCategoryCommandResult>>
{
    public async Task<Result<CreateCategoryCommandResult>> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        var category = request.Adapt<Category>();

        await _repository.AddAsync(category);

        if (category.Id <= 0)
        {
            return Result.Fail(new DetailError("Insert Category error"));
        }

        await _repository.SaveAsync();

        return new CreateCategoryCommandResult(category.Id);
    }
}
