namespace ProductService.Api.Models;
public class Product : IEntity
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? ImageId { get; set; }  
    public int? ImageThumbnailId { get; set; } 
    public decimal Price { get; set; }

    public virtual Category? Category { get; set; }
}
