namespace ProductService.Features.CreateCategory;

public class CreateCategoryValidation : AbstractValidator<CreateCategoryCommandRequest>
{
    public CreateCategoryValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(30);
    }
}
