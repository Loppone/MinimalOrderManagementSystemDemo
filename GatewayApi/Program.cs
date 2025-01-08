
using GatewayApi.Infrastructure;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ProductDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ProductConnectionString")));


        builder.Services.AddCarter();
        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

        //builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
        // builder.Services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
        //builder.Services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));

        //builder.Services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<,>));
        //builder.Services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<,>));

        builder.Services.AddScoped<IQueryRepository<Product>, QueryRepository<Product,ProductDbContext>>();
        builder.Services.AddScoped<ICommandRepository<Product>, CommandRepository<Product, ProductDbContext>>();


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        //    await InitializeDatabase.Seed(app);
        }

        app.UseHttpsRedirection();

        app.MapCarter();

        app.Run();
    }
}
