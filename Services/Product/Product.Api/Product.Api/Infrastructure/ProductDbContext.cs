namespace ProductService.Api.Infrastructure;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }

    public ProductDbContext()
    {
    }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Category> Categories { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        // Disabilito: preferisco passare 
        //modelBuilder.Entity<Product>()
        //    .Navigation(x=>x.Category)
        //    .AutoInclude();

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Category>().HasData(
            new     Category { Id = 1, Name = "Videogiochi e Console" },
            new Category { Id = 2, Name = "Elettronica e informatica" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Hogwarts Legacy PS5",
                Description = "Hogwarts Legacy è un gioco di ruolo d'azione open-world...",
                Price = 35,
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Name = "Console Playstation 5",
                Description = "La PlayStation 5 è una console per videogiochi prodotta da Sony...",
                Price = 400,
                CategoryId = 1
            },
            new Product
            {
                Id = 3,
                Name = "Tv Oled LG G4",
                Description = "Il TV OLED LG G4 è un televisore OLED 4K con pannello OLED...",
                Price = 4200,
                CategoryId = 2
            }
        );
    }
}
