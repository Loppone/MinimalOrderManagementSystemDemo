namespace ProductService.Api.Features.CreateCategory;

public class CreateCategoryValidation : AbstractValidator<CreateCategoryCommandRequest>
{
    public CreateCategoryValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name must not be empty")
            .MaximumLength(30)
            .WithMessage("Name can have a maximum of 30 characters");
    }
}
