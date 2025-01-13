namespace ProductService.Api.Features.CreateProduct;

public record CreateProductCommandRequest(
    int CategoryId, 
    string Name, 
    string Description, 
    decimal Price)
    : IRequest<Result<CreateProductCommandResult>>, IProductFields
{
//    public int CategoryId { get; set; } = CategoryId;

    public string Name { get; set; } = Name;
    public string Description { get; set; } = Description;
    public decimal Price { get; set; } = Price;
}
