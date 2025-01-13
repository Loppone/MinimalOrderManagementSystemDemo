using ProductService.Api.Application.Validation;

namespace ProductService.Api.Features.CreateProduct;

public class CreateProductValidation : ProductValidationUpsertBase<CreateProductCommandRequest>
{
    public CreateProductValidation()
    {
        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .WithMessage("Category Id must be greater than 0");
    }
}