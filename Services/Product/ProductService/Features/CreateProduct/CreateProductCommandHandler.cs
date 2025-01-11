namespace ProductService.Features.CreateProduct;

internal class CreateProductCommandHandler(ICommandRepository<Product> _repository)
    : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResult>
{
    public async Task<CreateProductCommandResult> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = request.Adapt<Product>();

        await _repository.AddAsync(product);

        if (product.Id <= 0)
        {
            throw new Exception("Failed to add product");
        }

        await _repository.SaveAsync();

        return new CreateProductCommandResult(product.Id);
    }
}
