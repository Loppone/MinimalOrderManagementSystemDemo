namespace ProductService.Features.CreateCategory;

public record CreateCategoryCommandRequest(string Name) : IRequest<Result<CreateCategoryCommandResult>>;
