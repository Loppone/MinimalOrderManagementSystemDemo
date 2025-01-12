
namespace ProductService.Features.GetProductById;

public class GetProductByIdQueryResult
{
    public Product? Product { get; set; }
    
    public GetProductByIdQueryResult()
    {
        
    }

    public GetProductByIdQueryResult(Product? product)
    {
        Product = product;
    }
}