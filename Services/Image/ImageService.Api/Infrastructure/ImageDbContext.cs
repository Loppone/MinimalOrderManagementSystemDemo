namespace ImageService.Api.Infrastructure;

public class ImageDbContext : DbContext
{
    public DbSet<Image> Images { get; set; }
}
