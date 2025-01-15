

namespace ProductService.Api.Features.DeleteProduct;

public class DeleteProductCommandHandler(
    ICommandRepository<Product> rwRepo,
    IQueryRepository<Product> rdRepo)
    : IRequestHandler<DeleteProductCommandRequest, Result<DeleteProductCommandResult>>
{
    public async Task<Result<DeleteProductCommandResult>> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await rdRepo.GetByIdAsync(request.Id, x => x.Category!);

        if (product is null)
            return Result.Fail(new DetailError("Product not found"));

        var result = await rwRepo.DeleteAsync(request.Id);

        return new DeleteProductCommandResult(result);
    }
}
