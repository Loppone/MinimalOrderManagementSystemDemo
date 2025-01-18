namespace ImageService.Api.Domain.Models;

public class Image : IEntity
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public EntityType TypeOfEntity { get; set; }
}
