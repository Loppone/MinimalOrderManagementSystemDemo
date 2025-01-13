namespace ProductService.Models;

public class Category : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [JsonIgnore] // Evita la ricorsione ciclica
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}