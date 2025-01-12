namespace ProductService.Features.CreateProduct;

public class CreateProductValidation : AbstractValidator<CreateProductCommandRequest>
{
    public CreateProductValidation()
    {
        RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Category Id must be greater than 0");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Description)
            .Must(x => x is null || x.Length > 5);
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}
