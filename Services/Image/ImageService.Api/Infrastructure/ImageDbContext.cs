namespace ImageService.Api.Infrastructure;

public class ImageDbContext : DbContext
{
    public ImageDbContext(DbContextOptions<ImageDbContext> options) : base(options)
    {
        
    }

    public virtual DbSet<Image> Images { get; set; }
}
