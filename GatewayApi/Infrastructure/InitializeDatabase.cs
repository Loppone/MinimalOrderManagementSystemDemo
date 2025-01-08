namespace GatewayApi.Infrastructure
{
    public static class InitializeDatabase
    {
        public static async Task Seed(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            // Product 
            await SeedProductDatabase(scope);
        }

        private static async Task SeedProductDatabase(IServiceScope scope)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
            if (!dbContext.Categories.Any())
            {
                dbContext.Categories.AddRange(
                    new Category
                    {
                        Id = 1,
                        Name = "Videogiochi e Console"
                    },
                    new Category
                    {
                        Id = 2,
                        Name = "Elettronica e informatica"
                    }
                );
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Products.Any())
            {
                dbContext.Products.AddRange(
                    new Product
                    {
                        Id = 1,
                        Name = "Hogwarts Legacy PS5",
                        Description = "Hogwarts Legacy è un gioco di ruolo d'azione open-world...",
                        Price = 40,
                        CategoryId = 2
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Console Playstation 5",
                        Description = "La PlayStation 5 è una console per videogiochi prodotta da Sony...",
                        Price = 482,
                        CategoryId = 2
                    },
                    new Product
                    {
                        Id = 3,
                        Name = "Tv Oled LG G4",
                        Description = "Il TV OLED LG G4 è un televisore OLED 4K con pannello OLED...",
                        Price = 300,
                        CategoryId = 3
                    }
                );

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
