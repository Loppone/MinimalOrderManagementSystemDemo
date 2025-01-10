using ProductService.Features.CreateProduct;

public class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ProductDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Product"),
                opt => opt.EnableRetryOnFailure())
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging());


        builder.Services.AddCarter();
        builder.Services.AddMediatR(cfg =>
        {
            //cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            cfg.RegisterServicesFromAssemblyContaining<CreateProductValidation>();

            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        builder.Services.AddValidatorsFromAssembly(typeof(CreateProductValidation).Assembly);


        builder.Services.AddScoped<IQueryRepository<Product>, QueryRepository<Product, ProductDbContext>>();
        builder.Services.AddScoped<ICommandRepository<Product>, CommandRepository<Product, ProductDbContext>>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            await app.Seed();
        }

        app.UseHttpsRedirection();

        app.MapCarter();

        app.Run();
    }
}
