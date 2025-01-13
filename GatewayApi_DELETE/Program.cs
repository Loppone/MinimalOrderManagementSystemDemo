public class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Database
        builder.Services.AddDbContext<ProductDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Product"),
                opt => opt.EnableRetryOnFailure())
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging());

        builder.Services.AddCarter();
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<CreateProductValidation>();
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        builder.Services.AddValidatorsFromAssembly(typeof(CreateProductValidation).Assembly);

        // Exception Handling
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        // Dependency Injection
        builder.Services.AddScoped<IQueryRepository<Product>, QueryRepository<Product, ProductDbContext>>();
        builder.Services.AddScoped<ICommandRepository<Product>, CommandRepository<Product, ProductDbContext>>();
        builder.Services.AddScoped<IQueryRepository<Category>, QueryRepository<Category, ProductDbContext>>();
        builder.Services.AddScoped<ICommandRepository<Category>, CommandRepository<Category, ProductDbContext>>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            await app.Seed();
        }

        app.UseHttpsRedirection();

        app.UseExceptionHandler(opt => { });

        app.MapCarter();

        app.Run();
    }
}
