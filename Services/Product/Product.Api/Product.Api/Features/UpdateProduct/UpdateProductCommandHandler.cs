namespace ProductService.Api.Features.UpdateProduct;

public class UpdateProductCommandHandler(
    ICommandRepository<Product> rwRepo,
    IQueryRepository<Product> rdRepo)
    : IRequestHandler<UpdateProductCommandRequest, Result<UpdateProductCommandResult>>
{
    public async Task<Result<UpdateProductCommandResult>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await rdRepo.GetByIdAsync(request.Id, x => x.Category!);

        if (product is null)
        {
            return Result.Fail(new DetailError("Product not found"));
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;

        var savedProduct = await rwRepo.UpdateAsync(product);

        return new UpdateProductCommandResult(savedProduct);
    }
}
