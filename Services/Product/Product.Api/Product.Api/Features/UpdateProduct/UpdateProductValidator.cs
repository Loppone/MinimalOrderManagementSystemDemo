namespace ProductService.Api.Features.UpdateProduct;

public class UpdateProductValidator 
    : ProductValidationUpsertBase<UpdateProductCommandRequest>
{
    public UpdateProductValidator()
    {
        // No rules, because they are already configued in the base class
    }
}
