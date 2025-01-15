namespace ProductService.Api.Features.DeleteProduct;

public class DeleteProductValidator : AbstractValidator<DeleteProductCommandRequest>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id Product must be greather than zero.");
    }
}
