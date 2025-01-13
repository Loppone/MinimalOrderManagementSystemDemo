namespace ProductService.Features.CreateProduct;

public class CreateProductValidation : AbstractValidator<CreateProductCommandRequest>
{
    public CreateProductValidation()
    {
        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .WithMessage("Category Id must be greater than 0");
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");
        RuleFor(x => x.Description)
            .Matches(@"^(?!\s*$).+")
            .WithMessage("Description cannot consist only of spaces.")
            .MinimumLength(5)
            .WithMessage("Description must be at least 5 characters long.")
            .When(x => !string.IsNullOrEmpty(x.Description)); 
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");
    }
}
