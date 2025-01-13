namespace ProductService.Api.Features.CreateCategory;

public record CreateCategoryCommandRequest(string Name) : IRequest<Result<CreateCategoryCommandResult>>;
