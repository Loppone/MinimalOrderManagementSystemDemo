namespace ProductService.Features.GetProducts;

public class GetProductsValidator : AbstractValidator<GetProductsQueryRequest>
{
    public GetProductsValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("Page number must be greater than 0");
        RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Page size must be greater than 0");
    }
}