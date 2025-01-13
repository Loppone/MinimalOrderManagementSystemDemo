namespace ProductService.Api.Features.UpdateProduct;

public record UpdateProductCommandRequest(
    int Id,
    string Name,
    string Description,
    decimal Price)
    : IRequest<Result<UpdateProductCommandResult>>, IProductFields
{
    public string Name { get; set; } = Name;
    public string Description { get; set; } = Description;
    public decimal Price { get; set; } = Price;
}