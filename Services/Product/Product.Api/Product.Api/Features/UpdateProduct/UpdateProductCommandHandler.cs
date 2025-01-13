
namespace ProductService.Api.Features.UpdateProduct;

public class UpdateProductCommandHandler(
    ICommandRepository<Product> rwRepo,
    IQueryRepository<Product> rdRepo) 
    : IRequestHandler<UpdateProductCommandRequest, Result<UpdateProductCommandResult>>
{
    public async Task<Result<UpdateProductCommandResult>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = rdRepo.GetByIdAsync(request.Id);
        
        if (product is null)
        {
            return Result.Fail(new DetailError("Product not found"));
        }

        var updatingProduct = request.Adapt(product);

        var savedProduct = await rwRepo.UpdateAsync(updatingProduct.Result!);

        return new UpdateProductCommandResult(savedProduct);
    }
}
